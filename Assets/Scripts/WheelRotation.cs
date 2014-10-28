using UnityEngine;
using System.Collections;

public class WheelRotation : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if ( CarDown.WheelRotation ) {
            transform.Rotate ( new Vector3(0,0,1) * Time.deltaTime*150 );
        }
	}
}
