using UnityEngine;
using System.Collections;

public enum ResourceRoot {
    Root ,
    JsonRoot ,
    PrefabRoot ,
    AnimationRoot ,
    TextureRoot,
    AudioRoot,
    CarModel,
    CarRun,
    Effect,
    Module
}


public class App : MonoBehaviour {

    private static App mgr;
    public static App Mgr {
        get {
            if ( mgr ) {
                return mgr;
            } else {
                GameObject go = Instantiate ( Resources.Load ( "App" ) ) as GameObject;
                go.name = "App";
                mgr = go.GetComponent<App> ( );
                return mgr;
            }
        }
    }

    public GameObject FingerGesturesPrefab;
 
    private float EscapeTime;

    #region 付费解锁

    public string KeyForAndroid;
    public string BundleIdentifier;
    public GameObject m_IABAndroidManager;
    public GameObject m_StoreKitManager;

    private bool PayLock = false;

#if UNITY_IPHONE
    private List<StoreKitProduct> _products;
#endif

    #endregion

    #region 配置数据

    public string[ ] CarNames;
    public int[ ] SceneID;
    public string[ ] SceneNames;

    #endregion

    #region 记录数据

    public int RateInterval {
        get { return PlayerPrefs.GetInt ( "RateInterval" , 0 ); }
        set { PlayerPrefs.SetInt ( "RateInterval" , value ); }
    }

    public class CarUnlock {
        public bool this[int id] {
            get {
                if ( id >= 0 && id < 4 ) {
                    return true;
                } else {
                    return PlayerPrefs.GetInt ( "CarUnlock" + id , 0 ) == 1;
                }
            }
            set {
                if ( value )
                    PlayerPrefs.SetInt( "CarUnlock" + id , 1 );
                else
                    PlayerPrefs.SetInt( "CarUnlock" + id , 0 );
            }
        }
    }
    public CarUnlock IsCarUnlock = new CarUnlock( );

    public class LevelPassed {
        public bool this[int id] {
            get { return PlayerPrefs.GetInt( "LevelPassed" + id , 0 ) == 1; }
            set {
                if ( value )
                    PlayerPrefs.SetInt( "LevelPassed" + id , 1 );
                else
                    PlayerPrefs.SetInt( "LevelPassed" + id , 0 );
            }
        }
    }
    public LevelPassed IsLevelPassed = new LevelPassed( );


    #endregion

    #region 运行数据

    public int CurrentPage;
    public int CurrentCar;
    public bool EP_EndFlag = false;

    #endregion

    #region 创建预制

    public GameObject CreateSwipeLock ( GameObject parent,System.Action passedHandle) {
        Object ob = LoadObject ( "SwipeLock" , ResourceRoot.PrefabRoot );
        GameObject go = NGUITools.AddChild ( parent , ob as GameObject );
        SwipeLock swipeLock =go.GetComponent<SwipeLock> ( );
        swipeLock.Init ( passedHandle );
        return go;
    }

    public GameObject CreateUnlock ( GameObject parent ) {
        Object ob = LoadObject ( "Unlock" , ResourceRoot.Module );
        GameObject go = NGUITools.AddChild ( parent , ob as GameObject );
        return go;
    }

#endregion

    void Awake ( ) {
        if ( !mgr ) { mgr = this; }
        DontDestroyOnLoad( gameObject );
        Instantiate( FingerGesturesPrefab );
        RateInterval++;
    }

    void Start ( ) {
        EscapeTime = 1;
    }

    void Update ( ) {
        EscapeTime += Time.deltaTime;
        if ( Input.GetKeyDown ( KeyCode.Escape ) && EscapeTime > 1 ) {
            EscapeTime = 0;
            switch ( Application.loadedLevelName ) {
                case "Logo":
                    break;
                case "Title":
                    Application.Quit ( );
                    break;
                case "Select":
                    Application.LoadLevel ( "Title" );
                    break;
                case "Assemble":
                    Application.LoadLevel ( "Select" );
                    break;
                case "E_DongXue":
                case "E_City":
                case "E_Hill":
                case "E_Road":
                    Application.LoadLevel ( "Select" );
                    break;
            }
        }
    }

    /// <summary>
    /// 初始化付费模块
    /// </summary>
    private void InitPrime31 ( ) {
#if UNITY_ANDROID
        //NGUIDebug.Log ( "IABAndroid.init begin" );
        Instantiate ( m_IABAndroidManager );
        IABAndroid.init ( KeyForAndroid );
        IABAndroidManager.purchaseSucceededEvent += ( str ) => {
            for ( int i = 0 ; i < 18 ; i++ ) {
                IsCarUnlock[i] = true;
            }
        };
        //NGUIDebug.Log ( "IABAndroid.init over" );
#elif UNITY_IPHONE
        Instantiate ( StoreKitManager );
        StoreKitManager.productListReceivedEvent += allProducts =>{
			_products = allProducts;
		};		
		var productIdentifiers = new string[] { BundleIdentifier };
		StoreKitBinding.requestProductData( productIdentifiers );
        StoreKitManager.purchaseSuccessfulEvent += ( transaction ) => {
            for ( int i = 0 ; i < 18 ; i++ ) {
                IsCarUnlock[i] = true;
            }
        };
#endif
    }

    /// <summary>
    /// 解锁应用
    /// </summary>
    /// <returns></returns>
    private IEnumerator unlockApp ( ) {
        if ( !PayLock ) {
            PayLock = true;
            InitPrime31 ( );
#if UNITY_ANDROID
            //NGUIDebug.Log ( "IABAndroid.purchaseProduct begin" );
            IABAndroid.purchaseProduct ( "full_version" );
            //NGUIDebug.Log ( "IABAndroid.purchaseProduct over" );
#elif UNITY_IPHONE
		if( _products != null && _products.Count > 0 ){
			int productIndex = 0;
			StoreKitProduct product = _products[productIndex];
			StoreKitBinding.purchaseProduct( product.productIdentifier, 1 );
		}
#endif
            yield return new WaitForSeconds ( 3 );
            PayLock = false;
        }
    }

    /// <summary>
    /// 解锁应用
    /// </summary>
    public void UnlockApp ( ) {
        StartCoroutine ( unlockApp ( ) );
    }


    private IEnumerator restoreApp ( ) {
        if ( !PayLock ) {
            PayLock = true;
#if UNITY_ANDROID
            IABAndroid.restoreTransactions ( );
#elif UNITY_IPHONE
		StoreKitBinding.restoreCompletedTransactions();
#endif
            yield return new WaitForSeconds ( 3 );
            PayLock = false;
        }
    }

    public void RestoreApp ( ) {
        StartCoroutine ( restoreApp ( ) );
    }

    /// <summary>
    /// 加载Resource下资源
    /// </summary>
    /// <param name="objectName"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public Object LoadObject ( string objectName , ResourceRoot type = ResourceRoot.Root ) {
        Object ob = null;
        switch ( type ) {
            case ResourceRoot.Root:
                break;
            case ResourceRoot.PrefabRoot:
                ob = Resources.Load( "Prefabs/" + objectName );
                break;
            case ResourceRoot.JsonRoot:
                ob = Resources.Load( "Jsons/" + objectName );
                break;
            case ResourceRoot.TextureRoot:
                ob = Resources.Load( "Textures/" + objectName );
                break;
            case ResourceRoot.AnimationRoot:
                ob = Resources.Load( "Animations/" + objectName );
                break;
            case ResourceRoot.AudioRoot:
                ob = Resources.Load( "Audios/" + objectName );
                break;
            case ResourceRoot.CarModel:
                ob = Resources.Load( "Prefabs/CarModels/" + objectName );
                break;
            case ResourceRoot.CarRun:
                ob = Resources.Load ( "Prefabs/CarRun/" + objectName );
                break;
            case ResourceRoot.Effect:
                ob = Resources.Load ( "Effects/" + objectName );
                break;
            case ResourceRoot.Module:
                ob = Resources.Load ( "Modules/" + objectName );
                break;
        }
        if ( ob == null ) {
            return null;
        } else {
            return ob;
        }
    }

    /// <summary>
    /// 释放资源占用的内存
    /// </summary>
    /// <param name="asset"></param>
    public void UnloadObject ( Object asset ) {
        Resources.UnloadAsset( asset );
    }

    /// <summary>
    /// 延迟加载关卡
    /// </summary>
    /// <param name="levelName"></param>
    /// <param name="waitTime"></param>
    /// <returns></returns>
    public IEnumerator LoadLevel ( string levelName , float waitTime = 0.1f ) {
        yield return new WaitForSeconds( waitTime );
        Application.LoadLevel( levelName );
    }

    /// <summary>
    /// 延迟异步加载关卡
    /// </summary>
    /// <param name="levelName"></param>
    /// <param name="waitTime"></param>
    /// <returns></returns>
    public IEnumerator LoadLevelAsync ( string levelName , float waitTime = 0.1f ) {
        yield return new WaitForSeconds ( waitTime );
        yield return Application.LoadLevelAsync ( levelName );
    }

    public AudioSource AddAudioAndPlay ( GameObject go , string name , bool loop = false , float volume = 1 ) {
        AudioClip audioClip = LoadObject ( name , ResourceRoot.AudioRoot ) as AudioClip;
        AudioSource audioSource = go.AddComponent<AudioSource> ( );
        audioSource.clip = audioClip;
        audioSource.loop = loop;
        audioSource.volume = volume;
        audioSource.Play ( );
        return audioSource;
    }

    public AudioSource AddAudio ( GameObject go , string name , bool loop = false , float volume = 1 ) {
        AudioClip audioClip = LoadObject ( name , ResourceRoot.AudioRoot ) as AudioClip;
        AudioSource audioSource = go.AddComponent<AudioSource> ( );
        audioSource.clip = audioClip;
        audioSource.loop = loop;
        audioSource.volume = volume;
        return audioSource;
    }

}

