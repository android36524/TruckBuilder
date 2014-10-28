using UnityEngine;
using System.Collections;

public class LuZhang : MonoBehaviour {

 
    void Start ( ) {

    }


    void Update ( ) {

    }

    void OnCollisionEnter2D ( Collision2D other ) {
        if ( other.gameObject.tag == "Player" || other.gameObject.name == "Collider" ) {
            Destroy ( gameObject , 1 );
        }
    }
}
