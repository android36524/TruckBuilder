using UnityEngine;
using System.Collections;

public class SwipeLock : MonoBehaviour {

    public string[ ] HintNames;
    public UISprite HintSprite;
    public int SwipeDistance = 200;
    public GameObject Close;

    private int randomNum;
    private bool directionCheck;

    private Vector2 startPos;
    private float swipeDistance;

    private System.Action passedHandle;

	void Start () {
        UIEventListener.Get ( Close ).onClick = ( go ) => { Destroy(gameObject); };
        randomNum = ( int )( Random.value * 100 %4 );
        HintSprite.spriteName=HintNames[randomNum];	
	}

    void OnEnable ( ) {
        FingerGestures.OnFingerSwipe += OnFingerSwipe;
        FingerGestures.OnFingerDown += OnFingerDown;
        FingerGestures.OnFingerUp += OnFingerUp;
    }

    void OnDisable ( ) {
        FingerGestures.OnFingerSwipe -= OnFingerSwipe;
        FingerGestures.OnFingerDown -= OnFingerDown;
        FingerGestures.OnFingerUp -= OnFingerUp;
    }


    void OnFingerDown ( int fingerIndex , Vector2 fingerPos ) {
        if ( fingerIndex == 0 ) {
            startPos = fingerPos;
        }
    }


    void OnFingerUp ( int fingerIndex , Vector2 fingerPos , float timeHeldDown ) {
        if ( fingerIndex == 0 ) {
            swipeDistance = Vector2.Distance ( startPos , fingerPos );
        }
    }

    void OnFingerSwipe ( int fingerIndex , Vector2 startPos , FingerGestures.SwipeDirection direction , float velocity ) {
        if ( swipeDistance > SwipeDistance ) {
            switch ( direction ) {
                case FingerGestures.SwipeDirection.Up:
                    if ( randomNum == 0 ) {
                        Destroy ( gameObject );
                        if ( passedHandle != null ) {
                            passedHandle ( );
                        }
                    }
                    break;
                case FingerGestures.SwipeDirection.Down:
                    if ( randomNum == 1 ) {
                        Destroy ( gameObject );
                        if ( passedHandle != null ) {
                            passedHandle ( );
                        }
                    }
                    break;
                case FingerGestures.SwipeDirection.Left:
                    if ( randomNum == 2 ) {
                        Destroy ( gameObject );
                        if ( passedHandle != null ) {
                            passedHandle ( );
                        }
                    }
                    break;
                case FingerGestures.SwipeDirection.Right:
                    if ( randomNum == 3 ) {
                        Destroy ( gameObject );
                        if ( passedHandle != null ) {
                            passedHandle ( );
                        }
                    }
                    break;
            }
            swipeDistance = 0;
        } else {
            swipeDistance = 0;
        }
    }

    public void Init ( System.Action _passedHandle ) {
        passedHandle = _passedHandle;
    }

}
