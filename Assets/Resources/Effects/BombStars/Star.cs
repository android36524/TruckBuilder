using UnityEngine;
using System.Collections;

public class Star : MonoBehaviour {

    private UISprite sprite;
    private bool triggerFlag;

    void Start ( ) {
        sprite = GetComponent<UISprite> ( );
        triggerFlag = false;
    }

   void OnTriggerEnter2D ( Collider2D other ) {
        if ( other.tag == "Player" ) {
            if ( !triggerFlag ) {
                triggerFlag = true;
                sprite.enabled = false;
                App.Mgr.AddAudioAndPlay ( gameObject , "race_sun01" );
                BroadcastMessage ( "BeginAnim" , other.gameObject );
            }
        }
    }
}
