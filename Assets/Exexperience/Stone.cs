using UnityEngine;
using System.Collections;

public class Stone : MonoBehaviour {

    private StoneItem[ ] StoneItems;
    private UISprite sprite;

    public GameObject ClickListener;
    public Collider2D C2D;

    void Start ( ) {
        sprite = gameObject.GetComponent<UISprite> ( );
        StoneItems = gameObject.GetComponentsInChildren<StoneItem> ( );
        foreach ( StoneItem stone in StoneItems ) {
            stone.gameObject.SetActive ( false );
        }
        UIEventListener.Get ( ClickListener ).onClick = ( go ) => {
            C2D.isTrigger = true;
            sprite.enabled = false;
            foreach ( StoneItem stone in StoneItems ) {
                if ( stone ) {
                    stone.gameObject.SetActive ( true );
                    stone.AutoBomb ( );
                }
            }
        };
    }

    void OnCollisionEnter2D ( Collision2D coll ) {
        if ( coll.gameObject.tag == "Player" ) {
            C2D.isTrigger = true;
            sprite.enabled = false;
            App.Mgr.AddAudioAndPlay ( gameObject , "car_hit" );
            foreach ( StoneItem stone in StoneItems ) {
                if ( stone ) {
                    stone.gameObject.SetActive ( true );
                    stone.RightBomb ( );
                }
            }
        }
    }


}
