using UnityEngine;
using System.Collections;

public class GenerateCar : MonoBehaviour
{
    public GameObject[] carList;
   
	void Start () {
        GameObject go = Instantiate( App.Mgr.LoadObject( App.Mgr.CarNames[App.Mgr.CurrentCar] , ResourceRoot.CarModel ) ) as GameObject;
        go.transform.parent = transform;
        go.transform.localScale = new Vector3(1, 1, 1);
        go.GetComponent<CarDown>().SetStartPos();
	}
	
}
