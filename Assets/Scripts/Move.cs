using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

    float fX;

    void Start ( ) {
        fX = 1024.0f * GameData.fWorldScaleW;
        if ( transform.localPosition.x == 1024 ) {
            transform.localPosition = new Vector3 ( fX , 0 );
        }
    }

    void Update ( ) {
        if ( GameData.bRun ) {
            transform.localPosition = new Vector3 ( transform.localPosition.x - GameData.fMoveYSpeed , transform.localPosition.y );
            if ( transform.localPosition.x <= -fX ) {
                transform.localPosition = new Vector3 ( fX , transform.localPosition.y );
            }
        }
    }

}
