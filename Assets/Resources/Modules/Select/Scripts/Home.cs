using UnityEngine;
using System.Collections;

public class Home : MonoBehaviour {

    public GameObject ButtonHome;
    public string LevelName;
    public float WaitTime;

    void Start ( ) {
        UIEventListener.Get( ButtonHome ).onClick = ( go ) => {
            StartCoroutine( App.Mgr.LoadLevel( LevelName , WaitTime ) );
        };
    }

}
