using UnityEngine;
using System.Collections;

public class RateView : MonoBehaviour {

    public GameObject Yes;
    public GameObject No;
    public GameObject Remind;

	void Start () {
        UIEventListener.Get( Yes ).onClick = RateOnClick;
        UIEventListener.Get( No ).onClick = RateOnClick;
        UIEventListener.Get( Remind ).onClick = RateOnClick;
	}

    void RateOnClick ( GameObject go ) {
        if ( go == Yes ) {
            App.Mgr.CreateSwipeLock ( transform.parent.gameObject , ( ) => {
                if ( Application.platform == RuntimePlatform.IPhonePlayer ) {
                    Application.OpenURL ( "http://itunes.apple.com/WebObjects/MZStore.woa/wa/viewContentsUserReviews?id=805979739&pageNumber=0&sortOrdering=1&type=Purple+Software&mt=8" );
                } else if ( Application.platform == RuntimePlatform.Android ) {
                    Application.OpenURL ( "https://play.google.com/store/apps/details?id=com.imayi.zombiefree" );
                } else {
                    Application.OpenURL ( "http://www.baidu.com" );
                }
                App.Mgr.RateInterval = 0;
            } );
        } else if ( go == No ) {
            App.Mgr.RateInterval = 0;
            Destroy ( gameObject );
        } else if ( go == Remind ) {
            App.Mgr.RateInterval = 2;
            Destroy ( gameObject );
        }
    }

}
