using UnityEngine;
using System.Collections;

public class ADView : MonoBehaviour {

    public GameObject Close;
    public GameObject AD1;
    public GameObject AD2;
    public GameObject AD3;
    public GameObject AD4;
    public GameObject AD5;
    public GameObject AD6;
	
	void Start () {
        UIEventListener.Get( Close ).onClick = ( go ) => { Destroy( gameObject ); };
        UIEventListener.Get( AD1 ).onClick = ADOnClick;
        UIEventListener.Get( AD2 ).onClick = ADOnClick;
        UIEventListener.Get( AD3 ).onClick = ADOnClick;
        UIEventListener.Get( AD4 ).onClick = ADOnClick;
        UIEventListener.Get( AD5 ).onClick = ADOnClick;
        UIEventListener.Get( AD6 ).onClick = ADOnClick;
	}

    void ADOnClick ( GameObject go ) {
        if ( go == AD1 ) {
            if ( Application.platform == RuntimePlatform.IPhonePlayer ) {
                Application.OpenURL( "https://itunes.apple.com/app/id629897226" );
            } else if ( Application.platform == RuntimePlatform.Android ) {
                Application.OpenURL( "https://play.google.com/store/apps/details?id=com.imayi.dinofree" );
            } else {
                Application.OpenURL( "http://www.baidu.com" );
            }
        } else if ( go == AD2 ) {
            if ( Application.platform == RuntimePlatform.IPhonePlayer ) {
                Application.OpenURL( "https://itunes.apple.com/app/id663827181" );
            } else if ( Application.platform == RuntimePlatform.Android ) {
                Application.OpenURL( "https://play.google.com/store/apps/details?id=com.imayi.dinoparkfree" );
            } else {
                Application.OpenURL( "http://www.baidu.com" );
            }
        } else if ( go == AD3 ) {
            if ( Application.platform == RuntimePlatform.IPhonePlayer ) {
                Application.OpenURL( "https://itunes.apple.com/app/id725956405" );
            } else if ( Application.platform == RuntimePlatform.Android ) {
                Application.OpenURL( "https://play.google.com/store/apps/details?id=com.imayi.iceagefree" );
            } else {
                Application.OpenURL( "http://www.baidu.com" );
            }
        } else if ( go == AD4 ) {
            if ( Application.platform == RuntimePlatform.IPhonePlayer ) {
                Application.OpenURL( "https://itunes.apple.com/app/id538790574" );
            } else if ( Application.platform == RuntimePlatform.Android ) {
                Application.OpenURL( "https://play.google.com/store/apps/details?id=com.imayi.earthfree" );
            } else {
                Application.OpenURL( "http://www.baidu.com" );
            }
        } else if ( go == AD5 ) {
            if ( Application.platform == RuntimePlatform.IPhonePlayer ) {
                Application.OpenURL( "https://itunes.apple.com/app/id583266996" );
            } else if ( Application.platform == RuntimePlatform.Android ) {
                Application.OpenURL( "https://play.google.com/store/apps/details?id=com.imayi.bunniesfree" );
            } else {
                Application.OpenURL( "http://www.baidu.com" );
            }
        } else if ( go == AD6 ) {
            if ( Application.platform == RuntimePlatform.IPhonePlayer ) {
                Application.OpenURL( "https://itunes.apple.com/app/id838688022" );
            } else if ( Application.platform == RuntimePlatform.Android ) {
                Application.OpenURL( "https://play.google.com/store/apps/details?id=com.tocakids.cookingfree" );
            } else {
                Application.OpenURL( "https://play.google.com/store/apps/details?id=com.tocakids.cookingfree" );
            }
        }
    }

}
