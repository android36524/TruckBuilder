using UnityEngine;
using System.Collections;

public class FallStar : MonoBehaviour {

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

    private IEnumerator Bomb ( float destroyTime = 1f ) {
        sprite.enabled = true;
        rigidbody2D.isKinematic = false;
        rigidbody2D.AddForce ( new Vector2 ( ( 0.5f - Random.value ) * 150 , Random.value * 100 ) );
        yield return new WaitForSeconds ( 0.1f );
        rigidbody2D.gravityScale = 0.2f;
        Destroy ( gameObject , destroyTime );
    }

    private void AutoBomb ( float destroyTime ) {
        StartCoroutine ( Bomb ( destroyTime ) );
    }

}
