using UnityEngine;
using System.Collections;

public class EPScene : MonoBehaviour {

    public Transform CarPos;
    public Transform CarPlane;
    public Camera MyCamera;


    void Start ( ) {

        switch ( Application.loadedLevelName ) {
            case "E_City":
                App.Mgr.AddAudioAndPlay ( gameObject , "114_loop4" , true );
                break;
            case "E_DongXue":
                App.Mgr.AddAudioAndPlay ( gameObject , "114_loop1" , true );
                break;
            case "E_Hill":
                App.Mgr.AddAudioAndPlay ( gameObject , "114_loop2" , true );
                break;
            case "E_Road":
                App.Mgr.AddAudioAndPlay ( gameObject , "114_loop3" , true );
                break;
        }

        string name = App.Mgr.CarNames[App.Mgr.CurrentCar];
        Object ob = App.Mgr.LoadObject ( name , ResourceRoot.CarRun );
        GameObject go = NGUITools.AddChild ( CarPlane.gameObject , ob as GameObject );
        go.transform.position = CarPos.transform.position;
        go.transform.localScale = CarPos.transform.localScale;
        MyCamera.GetComponent<CameraFollow> ( ).Target = go.transform;
    }
	
}
