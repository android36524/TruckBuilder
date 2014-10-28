using UnityEngine;
using System.Collections;

public class TiaoBan : MonoBehaviour {

    void OnTriggerEnter2D ( Collider2D other ) {
        if ( other.tag == "Player" ) {
            EPCar.car.BeginJumpAudio ( );
            if ( other.rigidbody2D ) {
                other.rigidbody2D.AddForce ( new Vector2 ( 0 , 2500 ) , ForceMode2D.Impulse );
                gameObject.collider2D.enabled = false;
            }
            if ( other.transform.parent.rigidbody2D ) {
                other.transform.parent.rigidbody2D.AddForce ( new Vector2 ( 1600 , 2000 ) , ForceMode2D.Impulse );
                gameObject.collider2D.enabled = false;
            }
        }
    }

}
