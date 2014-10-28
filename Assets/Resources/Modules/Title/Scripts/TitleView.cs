using UnityEngine;
using System.Collections;

public class TitleView : MonoBehaviour {

    public GameObject Play;
    public GameObject AD;

    void Start ( ) {

        App.Mgr.AddAudioAndPlay ( gameObject , "index_loop60s" , true );

        UIEventListener.Get ( Play ).onClick = ( go ) => {
            Animator amy = Play.GetComponent<Animator> ( );
            amy.enabled = false;
            StartCoroutine ( App.Mgr.LoadLevelAsync ( "Select" , 1f ) );
        };

        UIEventListener.Get ( AD ).onClick = ( go ) => {
            App.Mgr.CreateSwipeLock ( transform.parent.gameObject , ( ) => {
                Object ob = App.Mgr.LoadObject ( "ADView" , ResourceRoot.PrefabRoot );
                NGUITools.AddChild ( transform.parent.gameObject , ob as GameObject );
            } );
        };

        if ( App.Mgr.RateInterval >= 3 &&
            Application.internetReachability != NetworkReachability.NotReachable ) {
            Object ob = App.Mgr.LoadObject( "RateView" , ResourceRoot.PrefabRoot );
            NGUITools.AddChild( transform.parent.gameObject , ob as GameObject );
        }
    }

}
