using UnityEngine;
using System.Collections;

public class ZhuanKuai : MonoBehaviour {

    public string[ ] names;

    private BoxCollider2D collider;
    private UISprite sprite;

    void Start ( ) {
        collider = GetComponent<BoxCollider2D> ( );
        sprite = GetComponent<UISprite> ( );
        int i = ( int )( Random.value * 100 % names.Length );
        sprite.spriteName = names[i];
    }

}
