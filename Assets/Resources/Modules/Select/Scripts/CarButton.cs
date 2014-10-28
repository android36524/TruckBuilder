using UnityEngine;
using System.Collections;

public class CarButton : MonoBehaviour {

    private int id;
    private UISprite sprite;

    void Awake ( ) {
        sprite = GetComponent<UISprite>( );
    }

    public void Init ( int id ) {
        this.id = id;
        sprite.autoResizeBoxCollider = true;
        sprite.pivot = UIWidget.Pivot.Bottom;
        if ( App.Mgr.IsLevelPassed[id] ) {
            sprite.spriteName = "x_" + App.Mgr.CarNames[id];
        } else {
            sprite.spriteName = "x_" + App.Mgr.CarNames[id] + "_s";
        }
        sprite.MakePixelPerfect( );
        if ( App.Mgr.EP_EndFlag && App.Mgr.CurrentCar == id) {
            BroadcastMessage ( "SuccessEffect" );
            App.Mgr.EP_EndFlag = false;
        }
    }

    void OnClick ( ) {
        App.Mgr.CurrentCar = id;
        App.Mgr.AddAudioAndPlay ( gameObject , "truck_select" );
        StartCoroutine ( App.Mgr.LoadLevelAsync ( "Assemble" , 0.1f ) );
    }

}
