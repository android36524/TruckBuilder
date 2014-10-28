using UnityEngine;
using System.Collections;

public class Skin : MonoBehaviour
{
    public string[] spriteName;

    private UISprite sprite;
	
	void Start () {
        sprite = GetComponent<UISprite>();
        sprite.spriteName = spriteName[App.Mgr.CurrentCar/6];
        sprite.MakePixelPerfect();        
	}
	
}
