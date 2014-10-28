using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour
{
	
	// Update is called once per frame
	void Update () {
        //transform.localEulerAngles = new Vector3(transform.lo);
        if(GameData.bRun)
        transform.Rotate(new Vector3(0, 0, 1), 2);
	}
}
