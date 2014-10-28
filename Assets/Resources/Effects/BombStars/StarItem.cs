using UnityEngine;
using System.Collections;

public class StarItem : MonoBehaviour {

    public GameObject m_Target;
    public float Speed = 0.3f;

    private bool chaseFlag;
    private TweenPosition tweenPosition;
    private UISprite sprite;
    private Vector2 velocity;

    void Awake ( ) {
        sprite = GetComponent<UISprite> ( );
        sprite.enabled = false;
        tweenPosition = GetComponent<TweenPosition> ( );
        tweenPosition.enabled = false;
        tweenPosition.delay = 0;
        tweenPosition.style = TweenPosition.Style.Once;
        tweenPosition.AddOnFinished ( new EventDelegate ( this , "BeginChase" ) );
    }

    void Start ( ) {
        chaseFlag = false;
        m_Target = null;
    }

    void Update ( ) {
        if ( chaseFlag && m_Target != null ) {
            float tempX = Mathf.SmoothDamp ( transform.position.x , m_Target.transform.position.x , ref velocity.x , Speed  );
            float tempY = Mathf.SmoothDamp ( transform.position.y , m_Target.transform.position.y , ref velocity.y , Speed  );
            transform.position = new Vector3 ( tempX , tempY , transform.position.z );
            if ( Vector3.Distance ( transform.position , m_Target.transform.position ) < 0.8f ) {
                App.Mgr.AddAudioAndPlay ( transform.parent.gameObject , "race_sun02" , false , 0.05f );
                Destroy ( gameObject , 0.2f );
            }
        }
    }

    float FilterSpeed ( ) {
        float value = Random.value * 2;
        if ( value < 0.8 ) {
            return 0.8f;
        } else if ( value > 1.2 ) {
            return 1.2f;
        } else {
            return value;
        }
    }

    void BeginChase ( ) {
        StartCoroutine ( IE_BeginChase ( ) );
    }

    IEnumerator IE_BeginChase ( ) {
        yield return new WaitForSeconds ( Random.value * 0.2f );
        chaseFlag = true;
    }

    private void BeginAnim ( GameObject go ) {
        m_Target = go;
        tweenPosition.enabled = true;
        sprite.enabled = true;
    }

}
