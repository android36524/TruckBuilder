using UnityEngine;
using System.Collections;

public enum AssemblyState
{
    AssemblyState_Start,
    AssemblyState_End
}

public class GameData : MonoBehaviour {

    //屏幕宽的实际比例
    public static float fWorldScaleW;
    public static float fWorldScaleH;
    //是否运送零件
    public static bool bRun;
    //汽车拼装状态
    public static AssemblyState assemblyState;
    //输送带移动速度
    public static float fMoveYSpeed;

    void Awake ( ) {
        fWorldScaleH = ( float )Screen.height / 768.0f;
        float fW = 1024.0f * fWorldScaleH;
        fWorldScaleW = ( float )Screen.width / fW;

        assemblyState = AssemblyState.AssemblyState_Start;

        fMoveYSpeed = 10.0f * GameData.fWorldScaleW;
    }

    void Start ( ) {
        bRun = true;
        //if ( !App.Mgr.IsCarUnlock[App.Mgr.CurrentCar] ) {
        //    App.Mgr.CreateUnlock ( gameObject );
        //}
    }

}
