using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {
    IEnumerator Do ( ) {
        NGUIDebug.Log ( "1" );
        yield return new WaitForSeconds ( 2 );
        NGUIDebug.Log ( "2" );
    }
    IEnumerator newDo ( ) {
        NGUIDebug.Log ( "3" );
        yield return StartCoroutine ( Do ( ) );
        NGUIDebug.Log ( "4" );
    }
    void Awake ( ) {
        NGUIDebug.Log ( "5" );
        StartCoroutine ( Start ( ) );
        StartCoroutine ( newDo ( ) );
        NGUIDebug.Log ( "6" );
    }
    IEnumerator Start ( ) {
        NGUIDebug.Log ( "7" );
        yield return new WaitForSeconds ( 2 );
        NGUIDebug.Log ( "8" );
    }
    void OnEnable ( ) {
        NGUIDebug.Log ( "9" );
    }
}
