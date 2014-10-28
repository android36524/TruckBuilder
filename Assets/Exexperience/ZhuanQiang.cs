using UnityEngine;
using System.Collections;

public class ZhuanQiang : MonoBehaviour {

    private StoneItem[ ] StoneItems;
    private ZhuanKuai[ ] ZhuanKuais;

    public GameObject ClickListener;
    public Collider2D[ ] C2Ds;

    void Start ( ) {
        StoneItems = gameObject.GetComponentsInChildren<StoneItem> ( );
        ZhuanKuais = gameObject.GetComponentsInChildren<ZhuanKuai> ( );
        foreach ( StoneItem stone in StoneItems ) {
            stone.gameObject.SetActive ( false );
        }
        UIEventListener.Get ( ClickListener ).onClick = ( go ) => {
            foreach ( Collider2D c2d in C2Ds ) {
                c2d.isTrigger = true;
            }
            foreach ( ZhuanKuai zk in ZhuanKuais ) {
                zk.gameObject.SetActive ( false );
            }
            foreach ( StoneItem stone in StoneItems ) {
                if ( stone ) {
                    stone.gameObject.SetActive ( true );
                    stone.AutoBomb ( );
                }
            }
        };
    }

    void OnCollisionEnter2D ( Collision2D coll ) {
        if ( coll.gameObject.tag == "Player" ) {
            App.Mgr.AddAudioAndPlay ( gameObject , "car_hit" );
            foreach ( Collider2D c2d in C2Ds ) {
                c2d.isTrigger = true;
            }
            foreach ( ZhuanKuai zk in ZhuanKuais ) {
                zk.gameObject.SetActive ( false );
            }
            foreach ( StoneItem stone in StoneItems ) {
                if ( stone ) {
                    stone.gameObject.SetActive ( true );
                    stone.RightBomb ( );
                }
            }
        }
    }
}
