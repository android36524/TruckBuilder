using UnityEngine;
using System.Collections;

public class Logo : MonoBehaviour {

    public GameObject Yateland;

    void Update ( ) {
        if ( Input.GetMouseButtonUp ( 0 ) )
            Application.LoadLevel ( "Title" );
    }

    public void PlayYateland ( ) {
        Yateland.SetActive ( true );
        App.Mgr.AddAudioAndPlay ( Yateland , "yateland_brand" );
    }

    public void Load ( ) {
        Application.LoadLevel ( "Title" );
    }

}
