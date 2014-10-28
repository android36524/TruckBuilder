using UnityEngine;
using System.Collections;

public class FlashStar : MonoBehaviour {

    public float Duration = 0.25f;
    public float MaxSize = 1.3f;
    public float ContinueTime = 2.0f;

    private Vector3 OriginalScale;
    private UISprite sprite;

    void Start ( ) {
        sprite = GetComponent<UISprite> ( );
        sprite.enabled = false;
    }

    IEnumerator showFlashStars ( ) {

        //等待焊接动画播放完
        yield return new WaitForSeconds ( 0.6f );

        yield return new WaitForSeconds ( Random.value * ( ContinueTime - 4 * Duration ) );
        sprite.enabled = true;
        OriginalScale = transform.localScale = transform.localScale * RandomSize ( );
        transform.localScale = new Vector3 ( 1.3f * OriginalScale.x , 1.3f * OriginalScale.y , 1 );
        yield return new WaitForSeconds ( Duration );
        transform.localScale = new Vector3 ( OriginalScale.x , OriginalScale.y , 1 );
        yield return new WaitForSeconds ( Duration );
        transform.localScale = new Vector3 ( 1.3f * OriginalScale.x , 1.3f * OriginalScale.y , 1 );
        yield return new WaitForSeconds ( Duration );
        transform.localScale = new Vector3 ( OriginalScale.x , OriginalScale.y , 1 );
        yield return new WaitForSeconds ( Duration );
        Destroy ( gameObject );
    }

    void ShowFlashStars ( ) {
        StartCoroutine ( showFlashStars ( ) );
    }

    float RandomSize ( ) {
        float value = Random.value;
        if ( value < 0.3 ) {
            return 0.1f;
        } else if ( value > 0.7f ) {
            return 0.5f;
        } else {
            return 0.3f;
        }
    }

}
