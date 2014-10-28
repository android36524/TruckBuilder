using UnityEngine;
using System.Collections;

public class EPCar : MonoBehaviour {

    public GameObject[ ] Wheels;

    public static EPCar car;

    private float MaxSpeed = 1.5f;
    private float CarMass = 1000;
    private float Force = 0;
    private float MaxForce = 12000;

    public float[ ] HeightSubsections = new float[2];
    private bool touchFlag;
    private Vector3 touchDirection;
    private Vector3 touchDirectionRecord;

    private float tumbleTime;
    private float defaultTumbleTime = 3.0f;

    private bool isArrived = false;

    public bool AudioLoopFlag = false;
    private AudioSource AudioLoop;
    private AudioSource AudioBrake;
    private AudioSource AudioJump;

    void Start ( ) {

        AudioLoop = App.Mgr.AddAudio ( gameObject , "truck_loop" , true );
        AudioBrake = App.Mgr.AddAudio ( gameObject , "truck_brake" );
        AudioJump = App.Mgr.AddAudio ( gameObject , "truck_jump" );

        car = this;
        tumbleTime = 0;
        rigidbody2D.mass = CarMass;
        rigidbody2D.gravityScale = 1;
        rigidbody2D.angularDrag = 50f;
        rigidbody2D.drag = 0;
        rigidbody2D.interpolation = RigidbodyInterpolation2D.Extrapolate;
        touchFlag = false;
        touchDirection = Vector3.right;
    }

    void OnEnable ( ) {
        FingerGestures.OnFingerUp += OnFingerUp;
        FingerGestures.OnFingerDown += OnFingerDown;
    }

    void OnDisable ( ) {
        FingerGestures.OnFingerUp -= OnFingerUp;
        FingerGestures.OnFingerDown -= OnFingerDown;
    }

    void OnFingerDown ( int fingerIndex , Vector2 fingerPos ) {
        if ( fingerIndex == 0 ) {
            touchFlag = true;
            Force = 3000;
            CompareTouchDirection ( fingerPos );
        }        
    }

    void OnFingerUp ( int fingerIndex , Vector2 fingerPos , float timeHeldDown ) {
        if ( fingerIndex == 0 ) {
            touchFlag = false;
        }        
    }

    void FixedUpdate ( ) {
        if ( Force < MaxForce ) {
            Force+=100;
        } else {
            Force = MaxForce;
        }
        AdjustAudio ( );
        DriveWheels ( );
        if ( !isArrived ) {
            AdjustFaceDirection ( );
            AdjustTumble ( );
            if ( touchFlag ) {
                if ( Mathf.Abs ( rigidbody2D.velocity.x ) < MaxSpeed ) {
                    rigidbody2D.AddForce ( new Vector2 ( Force * touchDirection.x , 0 ) );
                }
            }
        }
    }

    void AdjustAudio ( ) {
        if ( !AudioLoopFlag ) {
            if ( AudioLoop.isPlaying && ( Mathf.Abs ( rigidbody2D.velocity.x ) < 0.2f || Mathf.Abs ( rigidbody2D.velocity.y ) > 0.5f ) ) {
                AudioLoop.Pause ( );
            } else if ( !AudioLoop.isPlaying && Mathf.Abs ( rigidbody2D.velocity.x ) > 0.2f && Mathf.Abs ( rigidbody2D.velocity.y ) < 0.5f ) {
                AudioLoop.Play ( );
            }
        }
    }

    void DriveWheels ( ) {
        foreach ( GameObject go in Wheels ) {
            go.transform.Rotate ( Vector3.forward * Time.deltaTime * rigidbody2D.velocity.x*300*touchDirection.x );
        }
    }

    void CompareTouchDirection ( Vector2 touchPosion ) {
        touchDirectionRecord = touchDirection;
        Vector3 worldPosion = GetWorldPos (touchPosion);
        if ( worldPosion.x < transform.position.x ) touchDirection = Vector3.left;
        if ( worldPosion.x > transform.position.x ) touchDirection = Vector3.right;
        if ( touchDirection != touchDirectionRecord ) {
            StartCoroutine ( AudioPlayTime ( AudioBrake , 0.5f ) );
        }
    }

    IEnumerator AudioPlayTime (AudioSource audioSource, float time ) {
        audioSource.time = 0;
        audioSource.Play ( );
        yield return new WaitForSeconds ( time );
        audioSource.Pause ( );
    }

    void AdjustTumble ( ) {
        if ( -30 < transform.eulerAngles.z && transform.eulerAngles.z < 30 ) {
            tumbleTime = 0;
        } else if ( 330 < transform.eulerAngles.z && transform.eulerAngles.z < 390 ) {
            tumbleTime = 0;
        }else{
            tumbleTime += Time.deltaTime;
        }
        if ( tumbleTime > defaultTumbleTime ) {
            rigidbody2D.MoveRotation ( -transform.localRotation.z);
            rigidbody2D.MovePosition ( transform.position );
        }
    }

    void AdjustFaceDirection ( ) {
        //按鼠标点击位置调整方向
        //NGUIDebug.Log ( "touchDirection" + touchDirection );
        if ( touchDirection == Vector3.left )
            transform.localScale = new Vector3 ( 1 , 1 , 1 );
        else
            transform.localScale = new Vector3 ( -1 , 1 , 1 );
        //按段调整方向
        //if ( transform.localPosition.y < HeightSubsections[0] && transform.localPosition.y > HeightSubsections[1] ) {
        //    faceDirection = 1;
        //    transform.localScale = new Vector3 ( 1 , 1 , 1 );
        //} else if ( transform.localPosition.y < HeightSubsections[1] ) {
        //    faceDirection = -1;
        //    transform.localScale = new Vector3 ( -1 , 1 , 1 );
        //}
    }

    Vector3 GetWorldPos ( Vector2 screenPos ) {
        Camera mainCamera = Camera.main;
        return mainCamera.ScreenToWorldPoint ( new Vector3 ( screenPos.x , screenPos.y , Mathf.Abs ( transform.position.z - mainCamera.transform.position.z ) ) );
    }

    public void BeginJumpAudio ( ) {
        AudioJump.Play ( );
    }

    public void Arrived ( ) {
        isArrived = true;
        AudioLoopFlag = true;
        AudioLoop.Pause ( );
        if ( Application.loadedLevelName == "E_Hill" ) {
            rigidbody2D.velocity = Vector2.right * 4.8f;
        } else {
            rigidbody2D.velocity = Vector2.right * 3f;
        }
        App.Mgr.AddAudioAndPlay ( gameObject , "truck_off" );
    }

}
