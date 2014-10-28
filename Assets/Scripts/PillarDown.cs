using UnityEngine;
using System.Collections;

public class PillarDown : MonoBehaviour {

    UISprite mySprite;
    Vector3 startPos;
    bool audioFlag = true;


    private float WaitTime;
    // Use this for initialization
    void Start ( ) {
        mySprite = GetComponent<UISprite> ( );
        startPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update ( ) {
        if ( GameData.assemblyState == AssemblyState.AssemblyState_End ) {
            WaitTime += Time.deltaTime;
            if ( WaitTime > 3.5f ) {
                if ( audioFlag ) {
                    App.Mgr.AddAudioAndPlay ( gameObject , "factory_conveyor2" );
                    audioFlag = false;
                }
                //改变对应汽车ID的拼装状态
                int carId = App.Mgr.CurrentCar;
                string str = "Cared" + carId;
                App.Mgr.IsLevelPassed[carId] = true;

                mySprite.fillAmount -= ( 1.0f / 136.0f );
                transform.localPosition = new Vector3 ( transform.localPosition.x , transform.localPosition.y - 1 );
                if ( transform.localPosition.y < startPos.y - 136 ) {
                    transform.localPosition = transform.localPosition = new Vector3 ( transform.localPosition.x , startPos.y - 136 );
                }
            }
        }
    }

}
