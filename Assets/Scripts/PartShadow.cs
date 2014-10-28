using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PartShadow : MonoBehaviour {

    public int PartId;

    public static List<PartShadow> CurrentPartShadow = new List<PartShadow> ( );

    //**********************ADD
    public bool IsWheel = false;
    public bool WheelRotation = false;
    public bool SimilarCover = false;

    public TweenAlpha tweenAlpha;
    private float defaultTime;
    private float calculateTime;

    public UISprite mySprite { get; set; }

    void Awake ( ) {
        defaultTime = 0.5f;
        tweenAlpha = gameObject.AddComponent<TweenAlpha> ( );
        mySprite = GetComponent<UISprite> ( );
        AlphaAnim ( );
    }

    void Update ( ) {

        calculateTime += Time.deltaTime;
        for ( int i = 0 ; i < CurrentPartShadow.Count ; i++ ) {
            if ( calculateTime > 4 * i * defaultTime && calculateTime < 4 * ( i + 1 ) * defaultTime ) {
                if ( CurrentPartShadow[i].tweenAlpha ) {
                    CurrentPartShadow[i].tweenAlpha.enabled = true;
                    InitAlpha ( i );
                }
            }
        }
        if ( calculateTime > 4 * CurrentPartShadow.Count * defaultTime ) {
            calculateTime = 0;
        }

    }

    public void InitAlpha ( int Except ) {
        for ( int i = 0 ; i < CurrentPartShadow.Count ; i++ ) {
            if ( i != Except ) {
                CurrentPartShadow[i].tweenAlpha.enabled = false;
                CurrentPartShadow[i].mySprite.alpha = 1f;
            }
        }
    }

    public void AlphaAnim ( ) {     
            tweenAlpha.enabled = true;
            tweenAlpha.from = 1;
            tweenAlpha.to = 0;
            tweenAlpha.method = UITweener.Method.EaseOut;
            tweenAlpha.style = UITweener.Style.PingPong;
            tweenAlpha.duration = defaultTime;
            tweenAlpha.ResetToBeginning ( );
    }

    public void StopAlphaAnim ( ) {
        mySprite.alpha = 1;
        tweenAlpha.enabled = false;
    }

}