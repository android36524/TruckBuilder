using UnityEngine;
using System.Collections;

public class SelectView : MonoBehaviour {

    public GameObject Page0;
    public GameObject Page7;

    public CarButton[ ] CarButtons;
    public GameObject[ ] Pages;
    public UISprite Background;
    public int SwipeSpeed = 400;
    public StageNumber MyStageNumber;

    private TweenPosition tweenPosition;
    private int currentPage;
    private int[ ] pagePosions;
    private bool swipeFlag;

    void Awake ( ) {
        tweenPosition = GetComponent<TweenPosition> ( );
    }

    void Start ( ) {

        App.Mgr.AddAudioAndPlay ( gameObject , "114_loop3" , true , 0.1f );

        swipeFlag = false;

        Page0.transform.localPosition = new Vector3 ( -Background.width,transform.localPosition.y );
        Page7.transform.localPosition = new Vector3 ( 6*Background.width , transform.localPosition.y );

        pagePosions = new int[Pages.Length];
        for ( int i = 0 ; i < pagePosions.Length ; i++ ) {
            pagePosions[i] = -i * Background.width;
        }
        currentPage = 0 ;
        for ( int i = 0 ; i < CarButtons.Length ; i++ ) {
            CarButtons[i].Init ( i );
        }
        for ( int i = 0 ; i < Pages.Length ; i++ ) {
            Pages[i].transform.localPosition = new Vector3 ( i * Background.width , 0 );
        }
        for ( int i = 0 ; i < CarButtons.Length ; i++ ) {
            if ( i % 3 == 0 )
                CarButtons[i].transform.localPosition = new Vector3 ( -Background.width / 4 , CarButtons[i].transform.localPosition.y );
            if ( i % 3 == 1 )
                CarButtons[i].transform.localPosition = new Vector3 ( Background.width / 4 , CarButtons[i].transform.localPosition.y );
        }

        //进入到离开时的页面
        currentPage = App.Mgr.CurrentPage;
        MyStageNumber.SetStage ( currentPage );
        PositionAnim ( Vector3.zero , new UnityEngine.Vector3 ( -currentPage * Background.width , transform.localPosition.y , transform.localPosition.z ) , 0 );
    }

    void OnEnable ( ) {
        FingerGestures.OnFingerSwipe += OnFingerSwipe;
        FingerGestures.OnFingerDragMove += OnFingerDragMove;
        FingerGestures.OnFingerDragEnd += OnFingerDragEnd;
    }

    void OnDisable ( ) {
        FingerGestures.OnFingerSwipe -= OnFingerSwipe;
        FingerGestures.OnFingerDragMove -= OnFingerDragMove;
        FingerGestures.OnFingerDragEnd -= OnFingerDragEnd;
    }


    void OnFingerDragEnd ( int fingerIndex , Vector2 fingerPos ) {
        PositionAnim ( transform.localPosition , new Vector3 ( pagePosions[currentPage] , 0 , 0 ) );
    }

    void OnFingerDragMove ( int fingerIndex , Vector2 fingerPos , Vector2 delta ) {
        transform.Translate ( new Vector3 ( delta.x , 0 , 0 ) * Time.deltaTime * 0.16f );
    }

    void OnFingerSwipe ( int fingerIndex , Vector2 startPos , FingerGestures.SwipeDirection direction , float velocity ) {
        if ( velocity > SwipeSpeed ) {
            if ( direction == FingerGestures.SwipeDirection.Left ) {
                if ( currentPage < Pages.Length - 1 ) {
                    PositionAnim ( new Vector3 ( transform.localPosition.x , 0 ) , new Vector3 ( pagePosions[currentPage + 1] , 0 ) );
                    currentPage++;
                }
            } else if ( direction == FingerGestures.SwipeDirection.Right ) {
                if ( currentPage > 0 ) {
                    PositionAnim ( new Vector3 ( transform.localPosition.x , 0 ) , new Vector3 ( pagePosions[currentPage - 1] , 0 ) );
                    currentPage--;
                }
            }
            MyStageNumber.SetStage ( currentPage );
            App.Mgr.CurrentPage = currentPage;
        }
    }

    void PositionAnim ( Vector3 from , Vector3 to , float time = 0.3f ) {
        tweenPosition.enabled = true;
        tweenPosition.from = from;
        tweenPosition.to = to;
        tweenPosition.method = UITweener.Method.EaseOut;
        tweenPosition.duration = time;
        tweenPosition.ResetToBeginning ( );
    }

}
