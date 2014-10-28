using UnityEngine;
using System.Collections;

public class CarDown : MonoBehaviour {
    public Vector3 birthPos;
    public int downMoveValue = 70;
    Vector3 startPos;

    public static bool WheelRotation = false;

    private float WaitTime;
    private bool MessageFlag = false;
    private bool audioFlag = true;

    void Start()
    {
        transform.localPosition = birthPos; 
        startPos = transform.localPosition;
    }


    void Update ( ) {
        if ( GameData.assemblyState == AssemblyState.AssemblyState_End ) {
            WaitTime += Time.deltaTime;
            if ( !MessageFlag ) {
                MessageFlag = true;
                BroadcastMessage ( "ShowFlashStars" );
            }
            if ( WaitTime > 3.5f ) {
                transform.localPosition = new Vector3 ( transform.localPosition.x , transform.localPosition.y - 1 );
                if ( transform.localPosition.y < startPos.y - downMoveValue ) {
                    WheelRotation = true;
                    if ( audioFlag ) {
                        audioFlag = false;
                        App.Mgr.AddAudioAndPlay ( gameObject , "truck_start" );
                    }
                    transform.localPosition = new Vector3 ( transform.localPosition.x , startPos.y - downMoveValue );
                    transform.Translate ( Vector3.left * Time.deltaTime * 0.8f );
                    if ( transform.localPosition.x < -1200 ) {
                        Application.LoadLevel ( App.Mgr.SceneNames[App.Mgr.SceneID[App.Mgr.CurrentCar] - 1] );
                        //Application.LoadLevel ( App.Mgr.SceneNames[App.Mgr.SceneID[App.Mgr.CurrentCar] - 1] );
                    }
                }
            }
        }
    }

    public void SetStartPos ( ) {
        WheelRotation = false;
        transform.localPosition = birthPos;
        startPos = transform.localPosition;
    }
}
