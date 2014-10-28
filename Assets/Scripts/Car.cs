using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Car : MonoBehaviour {
    public static List<PartShadow> partShadowList = new List<PartShadow>();

    //len 代表数组数量,里面存每个数组里存的数量
    public int[] arrayNumber;
    public int arrayIndex { get; set; }
    public int[][] towArray { get; set; }
    public static Car theCar;

    //*****************ADD
    public List<PartShadow> WheelList = new List<PartShadow> ( );
    public List<PartShadow> SimilarCoverList = new List<PartShadow> ( );
    public List<PartShadow> WheelRotation = new List<PartShadow> ( );

    Vector3 startPos;

    void Awake ( ) {

        theCar = this;
        for ( int i = 0 ; i < transform.childCount ; i++ ) {
            if ( transform.GetChild ( i ).name != "FlashStar" ) {
                PartShadow ps = transform.GetChild ( i ).GetComponent<PartShadow> ( );
                if ( ps )
                    partShadowList.Add ( ps );

                //*********************ADD
                if ( ps.IsWheel ) {
                    WheelList.Add ( ps );
                }
                if ( ps.WheelRotation ) {
                    WheelRotation.Add ( ps );
                }
                if ( ps.SimilarCover ) {
                    SimilarCoverList.Add ( ps );
                }
            }
        }

        //-------
        arrayIndex = 0;
        towArray = new int[arrayNumber.Length][ ];
        for ( int i = 0 ; i < arrayNumber.Length ; i++ ) {
            towArray[i] = new int[arrayNumber[i]];
        }
        int nIndex = 0;
        foreach ( int[ ] divInt in towArray ) {
            for ( int i = 0 ; i < divInt.Length ; i++ ) {
                divInt[i] = nIndex;
                nIndex++;
            }
        }
    }

	void Start () {
        startPos = transform.localPosition;
        PartShadow.CurrentPartShadow.Clear ( );
        showPartShadow();
	}

    //部件阴影初始化
    void showPartShadow ( ) {
        //匹配完成
        if ( arrayIndex >= towArray.Length ) {
            GameData.assemblyState = AssemblyState.AssemblyState_End;
            return;
        }
        //隐藏所有部件阴影
        for ( int i = 0 ; i < partShadowList.Count ; i++ ) {
            partShadowList[i].mySprite.enabled = false;
        }
        //按二维数组的层数显示阴影部件
        for ( int i = 0 ; i < towArray[arrayIndex].Length ; i++ ) {
            for ( int j = 0 ; j < partShadowList.Count ; j++ ) {
                if ( partShadowList[j].PartId == towArray[arrayIndex][i] ) {
                    partShadowList[j].mySprite.enabled = true;
                    //*****************ADD
                    PartShadow.CurrentPartShadow.Add ( partShadowList[j] );
                    partShadowList[j].mySprite.depth += 90;
                }
            }
        }
    }

    public int GetPartId(int index)
    {
        return towArray[arrayIndex][index];
    }

    public void ClearPartID(int Id)
    {
        foreach (int[] divInt in towArray)
        {
            for (int i = 0; i < divInt.Length; i++)
            {
                if (divInt[i] == Id)
                {
                    divInt[i] = -1;
                    break;
                }
            }
        }
        int nNum = 0;
        for (int i = 0; i < towArray[arrayIndex].Length;i++ )
        {
            if (towArray[arrayIndex][i] == -1)
            {
                nNum++;
            }
        }
        if (nNum == towArray[arrayIndex].Length)
        {
            arrayIndex++;
            PartShadow.CurrentPartShadow.Clear ( );
            showPartShadow();
            
        }
    }
    public int GetCurArrayLength()
    {
        return towArray[arrayIndex].Length;
    }

    //***************根据ID查找阴影
    public static PartShadow FindPartShadowToId(int Id)
    {
        for (int i = 0; i < partShadowList.Count;i++ )
        {
            if (partShadowList[i].PartId == Id)
            {
                return partShadowList[i];
            }
        }
        return null;
    }

    void OnDestroy()
    {
        partShadowList.Clear();
    }
}
