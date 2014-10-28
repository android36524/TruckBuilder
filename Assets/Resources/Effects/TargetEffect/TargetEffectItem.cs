using UnityEngine;
using System.Collections;

public class TargetEffectItem : MonoBehaviour {

    public string[ ] names;
    private UISprite sprite;

    void Awake ( ) {
        sprite = GetComponent<UISprite> ( );
        rigidbody2D.isKinematic = true;
        rigidbody2D.drag = 3;
        sprite.enabled = false;
    }

    void Start ( ) {
        int i = ( int )( Random.value * 100 % names.Length );
        sprite.spriteName = names[i];
    }

    void Update ( ) {
        //堆积在路面
        if ( transform.localPosition.y < -110 && Application.loadedLevelName != "E_Hill" ) {
            rigidbody2D.isKinematic = true;
            Destroy ( gameObject , 2 );
        }
    }

    IEnumerator Bomb ( ) {
        sprite.enabled = true;
        rigidbody2D.isKinematic = false;
        rigidbody2D.AddForce ( new Vector2 ( ( 0.5f - Random.value ) * 300 , Random.value * 400 ) );
        yield return new WaitForSeconds ( 0.3f );
        rigidbody2D.gravityScale = 0.2f;
        rigidbody2D.mass = 0.1f;
        if ( Application.loadedLevelName == "E_Hill" ) {
            Destroy ( gameObject , 2 );
        }
    }

    private void AutoBomb ( ) {
        StartCoroutine ( Bomb ( ) );
    }

}