using UnityEngine;
using System.Collections;

public enum PartState 
{
    PartState_Wait,
    PartState_Move,
    PartState_Conveyor,
    PartState_TouchMove,
    PartState_Car,
}
 
public class Part : MonoBehaviour
{
    //*********************ADD
    public bool IsWheel = false;
    public bool WheelRotation = false;
    public bool SimilarCover = false;
    private AudioSource audioSource;
    private bool audioFlag = true;

    public int PartId;
    Vector3 startPos;
    PartState state;
    bool bMove;
    Vector3 targetPos;
    TweenPosition tweenPos;
    TweenTransform tweenTrans;
    TweenScale tweenScale;
    public UISprite mySprite { get; set; }

    PartShadow myPartShadow;

    float moveDis;

    void Awake ( ) {
        tweenPos = GetComponent<TweenPosition> ( );
        tweenTrans = GetComponent<TweenTransform> ( );
        tweenScale = GetComponent<TweenScale> ( );
        mySprite = GetComponent<UISprite> ( );
    }

	void Start () {
        UIEventListener.Get(gameObject).onPress = OnTouchPress;
        UIEventListener.Get(gameObject).onDrag = OnTouchMove;

        startPos = transform.localPosition;
        state = PartState.PartState_Wait;
        transform.localScale = new Vector3(0.5f, 0.5f);

        myPartShadow = Car.FindPartShadowToId(PartId);

        moveDis = 0;

        //****************************ADD
        //碰撞器校正
        BoxCollider bc = GetComponent<BoxCollider> ( );
        if ( bc.size.x < 120 ) {
            bc.size = new Vector3 ( 120 , bc.size.y );
        }
        if ( bc.size.y < 120 ) {
            bc.size = new Vector3 ( bc.size.x , 120 );
        }

	}
	

	void Update () {

        switch (state)
        {
            case PartState.PartState_Wait:
            case PartState.PartState_Conveyor:

                //*******************************ADD
                //位置校正
                transform.localPosition = new Vector3  ( transform.localPosition.x, (0.5f * mySprite.width / mySprite.aspectRatio)*transform.localScale.x - 50 );

                if ( mySprite.width < 90 || mySprite.height < 90 ) {
                    transform.localScale = new UnityEngine.Vector3 ( 1f , 1f , 0 );
                } else if ( mySprite.width < 120 || mySprite.height < 120 ) {
                    transform.localScale = new UnityEngine.Vector3 ( 0.8f , 0.8f , 0 );
                } else {
                    transform.localScale = new UnityEngine.Vector3 ( 0.5f , 0.5f , 0 );
                }
                if (GameData.bRun)
                {
                    SetState(PartState.PartState_Move);
                }
                break;
            case PartState.PartState_Move:
                if ( audioFlag ) {
                   audioSource = App.Mgr.AddAudioAndPlay ( gameObject , "factory_conveyor1" );
                   audioFlag = false;
                }
                transform.localPosition = new Vector3 ( transform.localPosition.x - GameData.fMoveYSpeed , ( 0.5f * mySprite.width / mySprite.aspectRatio ) * transform.localScale.x - 50 );
                moveDis -= GameData.fMoveYSpeed;
                if (moveDis < -(1024f - 130) * GameData.fWorldScaleW)
                {
                    transform.localPosition = new Vector3 ( startPos.x - ( 1024f - 130 ) * GameData.fWorldScaleW , ( 0.5f * mySprite.width / mySprite.aspectRatio ) * transform.localScale.x - 50 );
                    startPos = transform.localPosition;
                    SetState(PartState.PartState_Conveyor);
                    audioSource.Stop ( );
                    audioFlag = true;
                    GameData.bRun = false;
                }
                break;
        }
	}

    void OnTouchPress ( GameObject sender , bool isPressed ) {

        if ( isPressed ) {
            App.Mgr.AddAudioAndPlay ( gameObject , "factory_press" );
        }

        //*************************设置层次***********************
        if ( state != PartState.PartState_Car ) {
            if ( isPressed ) {
                mySprite.depth += 60;
            } else {
                mySprite.depth -= 60;
            }
        }
        //*****************************部件识别******************
        if ( state == PartState.PartState_TouchMove && !isPressed ) {

            //**********************************ADD
            if ( IsWheel ) {
                bool flag = false;
                for ( int i = 0 ; i < Car.theCar.WheelList.Count ; i++ ) {
                    if ( Car.theCar.WheelList[i] ) {
                        float fDis = Vector3.Distance ( Car.theCar.WheelList[i].transform.position , transform.position );
                        if ( fDis < 0.15f ) {
                            tweenTrans.enabled = true;
                            tweenTrans.from = transform;
                            tweenTrans.to = Car.theCar.WheelList[i].transform;
                            tweenTrans.ResetToBeginning ( );
                            flag = true;
                            SetState ( PartState.PartState_Car );
                            SetScale ( new Vector3 ( 1 , 1 , 1 ) );
                            Car.theCar.WheelList[i].mySprite.depth -= 90;
                            Car.theCar.WheelList[i].StopAlphaAnim ( );
                            PartShadow.CurrentPartShadow.Remove ( Car.theCar.WheelList[i] );
                            Car.theCar.WheelList[i].BroadcastMessage ( "weldjointEffectBegin" );
                            Car.theCar.WheelList.RemoveAt ( i );
                            Car.theCar.ClearPartID ( PartId );
                            break;
                        } 
                    } else {
                        SetState ( PartState.PartState_Conveyor );
                        SetTargetPos ( startPos );
                    }
                }
                if ( !flag ) {
                    SetState ( PartState.PartState_Conveyor );
                    SetTargetPos ( startPos );
                }
            } else if ( SimilarCover ) {
                bool flag = false;
                for ( int i = 0 ; i < Car.theCar.SimilarCoverList.Count ; i++ ) {
                    if ( Car.theCar.SimilarCoverList[i] ) {
                        float fDis = Vector3.Distance ( Car.theCar.SimilarCoverList[i].transform.position , transform.position );
                        if ( fDis < 0.15f ) {
                            tweenTrans.enabled = true;
                            tweenTrans.from = transform;
                            tweenTrans.to = Car.theCar.SimilarCoverList[i].transform;
                            tweenTrans.ResetToBeginning ( );
                            flag = true;
                            SetState ( PartState.PartState_Car );
                            SetScale ( new Vector3 ( 1 , 1 , 1 ) );
                            Car.theCar.SimilarCoverList[i].mySprite.depth -= 90;
                            Car.theCar.SimilarCoverList[i].StopAlphaAnim ( );
                            PartShadow.CurrentPartShadow.Remove ( Car.theCar.SimilarCoverList[i] );
                            Car.theCar.SimilarCoverList[i].BroadcastMessage ( "weldjointEffectBegin" );
                            Car.theCar.SimilarCoverList.RemoveAt ( i );
                            Car.theCar.ClearPartID ( PartId );
                            break;
                        }
                    } else {
                        SetState ( PartState.PartState_Conveyor );
                        SetTargetPos ( startPos );
                    }
                }
                if ( !flag ) {
                    SetState ( PartState.PartState_Conveyor );
                    SetTargetPos ( startPos );
                }

            } else {
                if ( myPartShadow ) {
                    float fDis = Vector3.Distance ( myPartShadow.transform.position , transform.position );
                    if ( fDis < 0.15f ) {
                        SetState ( PartState.PartState_Car );
                        SetTargetPos ( );
                        myPartShadow.mySprite.depth -= 90;
                        myPartShadow.StopAlphaAnim ( );
                        PartShadow.CurrentPartShadow.Remove(myPartShadow) ;
                        myPartShadow.BroadcastMessage ( "weldjointEffectBegin" );
                        Car.theCar.ClearPartID ( PartId );
                    } else {
                        SetState ( PartState.PartState_Conveyor );
                        SetTargetPos ( startPos );
                    }
                } else {
                    SetState ( PartState.PartState_Conveyor );
                    SetTargetPos ( startPos );
                }
            }
        }
    }

    void OnTouchMove(GameObject sender, Vector2 delta)
    {
        delta = delta / GameData.fWorldScaleH;
        if ( state == PartState.PartState_Conveyor ) {
            SetState ( PartState.PartState_TouchMove );
            SetScale ( new Vector3 ( 1 , 1 , 1 ) );
        }
        if ( state == PartState.PartState_TouchMove ) {
            transform.localPosition = new Vector3 ( transform.localPosition.x + delta.x , transform.localPosition.y + delta.y );
        }
    }

    void SetTargetPos(Vector3 tarPos)
    {
        tweenPos.enabled = true;
        tweenPos.from = transform.localPosition;
        tweenPos.to = tarPos;
        
        tweenPos.ResetToBeginning();

        SetScale(new Vector3(0.5f, 0.5f, 0.5f));

    }

    void SetTargetPos( )
    {
        if (myPartShadow)
        {
            tweenTrans.enabled = true;
            tweenTrans.from = transform;
            tweenTrans.to = myPartShadow.transform;

            tweenTrans.ResetToBeginning();
        }  
    }
    void SetScale(Vector3 scale)
    {
        tweenScale.enabled = true;
        tweenScale.from = transform.localScale;
        tweenScale.to = scale;

        tweenScale.ResetToBeginning();

    }
   
    public void SetState(PartState tempstate)
    {
        state = tempstate;
    }
}
