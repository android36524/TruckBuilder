using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Vector2 DefaultResolution = new Vector2 ( 1024 , 768 );

    public float xSmooth = 8f;
    public float ySmooth = 8f;
    public Vector2 maxXAndY;
    public Vector2 minXAndY;

    public Transform Target;

    private Vector2 velocity;

    void Start ( ) {
        maxXAndY.x -= ( camera.aspect - DefaultResolution.x / DefaultResolution.y ) * 400;
        minXAndY.x += ( camera.aspect - DefaultResolution.x / DefaultResolution.y ) * 400;
    }

    void LateUpdate ( ) {
        FollowTarget ( );
    }

    void FollowTarget ( ) {
        float tempX = Mathf.Lerp ( transform.localPosition.x , Target.localPosition.x ,Time.time);
        float tempY = Mathf.Lerp ( transform.localPosition.y , Target.localPosition.y ,Time.time);
       if ( tempX > maxXAndY.x ) {
           tempX = maxXAndY.x;
       }
       if ( tempX < minXAndY.x ) {
           tempX = minXAndY.x;
       }
       if ( tempY > maxXAndY.y ) {
           tempY = maxXAndY.y;
       }
       if ( tempY < minXAndY.y ) {
           tempY = minXAndY.y;
       }
       transform.localPosition = new Vector3 ( tempX , tempY , transform.localPosition.z );
    }

}
