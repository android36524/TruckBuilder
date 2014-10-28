using UnityEngine;
using System.Collections;

public class jianju : MonoBehaviour {

    Vector3 startPos;
	// Use this for initialization
	void Start () {
        startPos = transform.localPosition;
        transform.localPosition = new Vector3(transform.localPosition.x * GameData.fWorldScaleW,0);
	}
	
	// Update is called once per frame
	void Update () {
        transform.localPosition = new Vector3(startPos.x * GameData.fWorldScaleW, 0);
	}
}
