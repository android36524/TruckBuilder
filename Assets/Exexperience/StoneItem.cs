using UnityEngine;
using System.Collections;

public class StoneItem : MonoBehaviour {

    void Awake ( ) {
        rigidbody2D.isKinematic = true;
        collider2D.isTrigger = true;
    }

    public void AutoBomb ( ) {
        rigidbody2D.isKinematic = false;
        collider2D.isTrigger = false;
        rigidbody2D.AddForce ( new Vector2 ( ( 0.5f - Random.value ) * 300 , Random.value * 100 ) );
        transform.localScale = new Vector3 ( ( Random.value + 0.2f ) * 1.6f , ( Random.value + 0.2f ) * 1.6f , 1 );
        Destroy ( gameObject , 2 );
    }

    public void RightBomb ( ) {
        rigidbody2D.isKinematic = false;
        collider2D.isTrigger = false;
        rigidbody2D.AddForce ( new Vector2 ( ( Random.value ) * 200 , Random.value * 100 ) );
        transform.localScale = new Vector3 ( ( Random.value + 0.2f ) * 1.6f , ( Random.value + 0.2f ) * 1.6f , 1 );
        Destroy ( gameObject , 2 );
    }

}
