using UnityEngine;
using System.Collections;

public class CarPartManage : MonoBehaviour {

    public GameObject carPart;
    public UIAtlas atlas;
    public string[ ] PartName;

    int nIndex;

    void Start ( ) {
        nIndex = Car.theCar.arrayIndex;
        CreatePart ( );
    }

    void Update ( ) {
        if ( nIndex != Car.theCar.arrayIndex && Car.theCar.arrayIndex < Car.theCar.towArray.Length ) {
            CreatePart ( );
            nIndex = Car.theCar.arrayIndex;
        }
    }

    //*********************************部件初始化*****************************
    void CreatePart ( ) {
        int Length = Car.theCar.GetCurArrayLength ( );
        for ( int i = 0 ; i < Length ; i++ ) {
            GameObject go = Instantiate ( carPart ) as GameObject;
            go.transform.parent = transform;
            go.transform.localPosition = new Vector3 ( 80 * Camera.main.aspect + ( 500 / Length*Camera.main.aspect ) * i , 0 );
            Part part = go.GetComponent<Part> ( );
            part.PartId = Car.theCar.GetPartId ( i );
            part.mySprite.atlas = atlas;
            part.mySprite.spriteName = PartName[part.PartId];
            part.mySprite.MakePixelPerfect ( );
            part.mySprite.depth = 60 + part.PartId;

            //******************ADD
            foreach ( PartShadow ps in Car.theCar.WheelList ) {
                if ( part.PartId == ps.PartId ) {
                    part.IsWheel = true;
                }
            }
            foreach ( PartShadow ps in Car.theCar.WheelRotation ) {
                if ( part.PartId == ps.PartId ) {
                    part.gameObject.AddComponent<WheelRotation> ( );
                }
            }
            foreach ( PartShadow ps in Car.theCar.SimilarCoverList ) {
                if ( part.PartId == ps.PartId ) {
                    part.SimilarCover = true;
                }
            }

        }
        GameData.bRun = true;
    }

}
