using UnityEngine;
using System.Collections;

public class FallStarsEffect : MonoBehaviour {

    public GameObject Star;
    public GameObject Light;

    void Awake ( ) {
        Light.SetActive ( false );
    }

	private IEnumerator BeginFall () {
        for ( int i = 0 ; i < 20 ; i++ ) {
            GameObject go = NGUITools.AddChild ( transform.gameObject , Star );
            go.transform.localPosition = new Vector3 ( 0 , 85 );
        }
        yield return new WaitForSeconds ( 0.3f );
        BroadcastMessage ( "AutoBomb",1.0f );
        yield return new WaitForSeconds ( 1f );
        Light.SetActive ( true );
        Destroy ( Light , 3 );
	}

    private void SuccessEffect ( ) {
        StartCoroutine ( BeginFall ( ) );
    }

}
