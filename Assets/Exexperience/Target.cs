using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {

    void OnTriggerEnter2D ( Collider2D other ) {
        if ( other.tag == "Player" ) {
            App.Mgr.AddAudioAndPlay ( gameObject , "cheers" );
            BroadcastMessage ( "BeginTargetEffect" );
            App.Mgr.EP_EndFlag = true;
            StartCoroutine ( App.Mgr.LoadLevel ( "Select" , 3 ) );
            gameObject.collider2D.enabled = false;
            EPCar.car.Arrived ( );
        }
    }

}
