using UnityEngine;
using System.Collections;

public class WeldjointEffect : MonoBehaviour {

    public string sFormat = "name{0}{1}";
    public int iStart = 1;
    public int iCount = 12;
    public float fWaitSecond = 0.1f;
    public float continueTime = 0.6f;

    private UISprite m_Sprite;
    private int m_iCur = 0;

    void Awake ( ) {
        m_Sprite = GetComponent<UISprite> ( );
        m_Sprite.depth += 300;
        m_Sprite.enabled = false;
        m_iCur = iStart;
    }

    IEnumerator Anim ( ) {
        while ( true ) {
            if ( m_iCur < iCount + iStart ) {
                string sName = string.Format ( sFormat , m_iCur / 10 , m_iCur % 10 );
                m_Sprite.spriteName = sName;
                m_Sprite.MakePixelPerfect ( );
                m_iCur++;
            } else {
                m_iCur = iStart;
            }
            yield return new WaitForSeconds ( fWaitSecond );
        }
    }

    private void weldjointEffectBegin ( ) {
        App.Mgr.AddAudioAndPlay ( gameObject , "welding" );
        m_Sprite.enabled = true;
        StartCoroutine ( "Anim" );
        Destroy ( gameObject , continueTime );
    }

}
