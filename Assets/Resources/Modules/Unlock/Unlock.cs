using UnityEngine;
using System.Collections;

public class Unlock : MonoBehaviour {

    public GameObject Close;
    public GameObject Get;
    public GameObject Restore;

    void Start ( ) {
        UIEventListener.Get ( Close ).onClick += ( go ) => {
            Application.LoadLevel ( "Select" );
        };
        UIEventListener.Get ( Get ).onClick += ( go ) => {
            App.Mgr.UnlockApp ( ); };
        UIEventListener.Get ( Restore ).onClick += ( go ) => { 
            App.Mgr.RestoreApp ( ); };
    }

}
