using UnityEngine;
using System.Collections;

public class TargetEffect : MonoBehaviour {

    public GameObject TargetEffectItem;

    private IEnumerator BeginFall ( ) {
        for ( int i = 0 ; i < 80 ; i++ ) {
            GameObject go = NGUITools.AddChild ( transform.gameObject , TargetEffectItem );
            go.transform.localPosition = new Vector3 ( 0 , 30 );
            go.GetComponent<UISprite> ( ).MakePixelPerfect ( );
        }
        BroadcastMessage ( "AutoBomb" , 2f );
        yield return new WaitForSeconds ( 0.3f );
    }

    private void BeginTargetEffect ( ) {
        StartCoroutine ( BeginFall ( ) );
    }

}
