using UnityEngine;
using System.Collections.Generic;


public class IABUIManager : MonoBehaviour
{
#if UNITY_ANDROID
	void OnGUI()
	{
		float yPos = 5.0f;
		float xPos = 5.0f;
		float width = ( Screen.width >= 800 || Screen.height >= 800 ) ? 320 : 160;
		float height = ( Screen.width >= 800 || Screen.height >= 800 ) ? 80 : 40;
		float heightPlus = height + 10.0f;
	
	
		if( GUI.Button( new Rect( xPos, yPos, width, height ), "Initialize IAB" ) )
		{
			var key = "your public key from the Android developer portal here";
			IABAndroid.init( key );
		}


		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Start Billing Availability Check" ) )
		{
			IABAndroid.startCheckBillingAvailableRequest();
		}

	
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Test Purchase" ) )
		{
			IABAndroid.testPurchaseProduct();
		}
	
	
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Test Refund" ) )
		{
			IABAndroid.testRefundedProduct();
		}
	
	
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Restore Transactions" ) )
		{
			IABAndroid.restoreTransactions();
		}
		
		
		xPos = Screen.width - width - 5.0f;
		yPos = 5.0f;

		if( GUI.Button( new Rect( xPos, yPos, width, height ), "Purchase Real Product" ) )
		{
			IABAndroid.purchaseProduct( "com.prime31.testproduct" );
		}


		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Stop Billing Service" ) )
		{
			IABAndroid.stopBillingService();
		}
	}
#endif
}
