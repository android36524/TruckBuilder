using UnityEngine;
using System.Collections;

public class StageNumber : MonoBehaviour {

    public UISprite Sprite;
    public string[ ] SpriteNames;

    private int currentStage;

    void Awake ( ) {
        currentStage = 0;
        Sprite.spriteName = SpriteNames[currentStage];
    }

    public void SetStage ( int number ) {
        currentStage = number;
        Sprite.spriteName = SpriteNames[number];
    }

    public int GetCurrentStage ( ) {
        return currentStage;
    }

}
