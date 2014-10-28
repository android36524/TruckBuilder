using UnityEngine;
using System.Collections;

public class SpriteAnim : MonoBehaviour {

    public string sFormat = "name{0}{1}";//精灵名字格式   
    public int iStart = 1;
    public int iCount = 12;
    public float fWaitSecond = 0.15f;//每帧停留时间   
    private UISprite m_Sprite;
    private int m_iCur = 0;

    void Awake ( ) {
        m_Sprite = GetComponent<UISprite> ( );
        m_iCur = iStart;
    }

    void OnEnable ( ) {
        m_Sprite.enabled = true;
        StartCoroutine ( "Anim" );
    }

    void OnDisable ( ) {
        m_Sprite.enabled = false;
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

}