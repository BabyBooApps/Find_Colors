using UnityEngine;
using System.Collections;

public class FloatingObj : MonoBehaviour {

	float screenX ;
	float screenY ;
	float leftMargin ;
	float rightMargin ;
	float topMargin ;

	float bottomMargin ;

	bool touched = false;

	void Start () 
	{
		iTween.FadeFrom(gameObject, 0f, 0.5f);
		screenY = (Screen.height * 5) / 100;
		screenX = (Screen.width * 10) / 100;
		//Debug.Log ("screenx: " + screenX);
		//Debug.Log ("screeny: " + Screen.height);

		leftMargin = Screen.width / 5f;
		rightMargin = Screen.width / 14f;
		topMargin = Screen.height / 4f;
		bottomMargin = Screen.height / 14f;
	//	Debug.Log ("screeny: " + screenY);



		StartCoroutine (MoveIt());

	}

	void Update()
	{
		Vector3 playerPosScreen;
		// Check if the player's ship is on screen.        
		playerPosScreen = Camera.main.WorldToScreenPoint(transform.position);
		if (playerPosScreen.y < -(screenY - bottomMargin))
		{
			transform.position = Camera.main.ScreenToWorldPoint(
				new Vector3(playerPosScreen.x ,-(screenY - bottomMargin),playerPosScreen.z));
		}
		else if (playerPosScreen.y > Screen.height - screenY - topMargin)
		{
			transform.position = Camera.main.ScreenToWorldPoint(
				new Vector3(playerPosScreen.x,Screen.height - screenY - topMargin,playerPosScreen.z));
		}
		
		// X - axis ( Horizontal on screen)
		if (playerPosScreen.x < screenX + leftMargin )
		{
			//Debug.Log ("playerPosScreen.x: " + playerPosScreen.x);
			transform.position = Camera.main.ScreenToWorldPoint(
				new Vector3(screenX + leftMargin,
									playerPosScreen.y,
									playerPosScreen.z));
		}
		else if (playerPosScreen.x > Screen.width  - screenX - rightMargin )
		{
			//Debug.Log ("playerPosScreen.x: " + playerPosScreen.x);
			transform.position = Camera.main.ScreenToWorldPoint(
						new Vector3(Screen.width - screenX - rightMargin,
									playerPosScreen.y,
									playerPosScreen.z));
		}

	}
	
	IEnumerator MoveIt()
	{	

		while ( true )
		{

			/*
			 if(touched){

				StartCoroutine (MoveItFast());
				yield return new WaitForSeconds(5f);
				touched = false;
				gameObject.GetComponent<PolygonCollider2D>().enabled = true;

			}else{
			*/
				//gameObject.GetComponent<Rigidbody2D>().velocity = gameObject.GetComponent<Rigidbody2D>().velocity 
				gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(UnityEngine.Random.Range(-2F,2F),UnityEngine.Random.Range(-2F,2F)) ;			
				yield return new WaitForSeconds(0.5f);
			//}
		}
	}

	IEnumerator MoveItFast()
	{	
		
		while ( touched )
		{
			//gameObject.GetComponent<Rigidbody2D>().velocity = gameObject.GetComponent<Rigidbody2D>().velocity 
			float x = 0;
			float y = 0;
			for (int i=0; i<2; i++ )
			{
			float num = UnityEngine.Random.Range(5f,15f); // 5 .. 15 
			if (num > 10f)
				num = 6f-num; // (6-11) .. (6-15) == -5 .. -10

				if(i==0) x = num;
				else y = num;

			}
			gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(x,y) ;			
			//gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(UnityEngine.Random.Range(-10F,10F),UnityEngine.Random.Range(-10F,10F)) ;			
			yield return new WaitForSeconds(0.05f);
			//yield return new WaitForEndOfFrame();

		}
	}

	public void SetAsTouched()
	{
		touched = true;
	}

}
