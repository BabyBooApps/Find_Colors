using UnityEngine;
using System.Collections;

public class CameraResize : MonoBehaviour 
{

	public float orthographicSize = 5;
	public float aspect = 1.55f;

	Camera cam;
	public GameObject background;

	void Start()
	{
		cam = Camera.main;
        /*
		if (StaticArrays.aspect == 0f) {
			float screenAspect = (float)Screen.width/(float)Screen.height;			
			if (screenAspect > 1.5f) {
				aspect = screenAspect;
			}
			StaticArrays.aspect = aspect;
		//	Debug.Log(StaticArrays.aspect);
		}
		aspect = StaticArrays.aspect;

		Camera.main.projectionMatrix = Matrix4x4.Ortho(
			-orthographicSize * aspect, orthographicSize * aspect,
			-orthographicSize, orthographicSize,
			GetComponent<Camera>().nearClipPlane, GetComponent<Camera>().farClipPlane);

		//aspect = Camera.main.aspect;
		//Debug.Log(aspect);
        */

		Resize ();

        /*
		if (StaticArrays.leftX == 0f) {
			//Debug.Log (cam.orthographicSize * aspect);
			StaticArrays.leftX = -(cam.orthographicSize * aspect) + 0.6f ;
			//Debug.Log (StaticArrays.leftX);
		}
        */
	}


	
	void Resize()
	{
		SpriteRenderer sr = background.GetComponent<SpriteRenderer>();
		if(sr==null) return;

        aspect = (float)Screen.width / (float)Screen.height;

        float worldScreenHeight = 2f * cam.orthographicSize;
		//float worldScreenWidth = worldScreenHeight * cam.aspect;
		float worldScreenWidth = 2f * cam.orthographicSize * aspect;

        /*
		//float worldScreenHeight = Camera.main.orthographicSize * 2;
		//float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;		
		background.transform.localScale = new Vector3(worldScreenWidth / sr.sprite.bounds.size.x,
		                                   worldScreenHeight / sr.sprite.bounds.size.y, 1);
		*/


        float widthScale = (float)worldScreenWidth / (float)sr.sprite.bounds.size.x;
        float heightScale = (float) worldScreenHeight / (float)sr.sprite.bounds.size.y ;		


		background.transform.localScale = new Vector3(widthScale * 1.5f, heightScale * 1.5f, 1);
		/*
		if(widthScale > heightScale)
			background.transform.localScale = new Vector3(widthScale, widthScale, 1);
		else
			background.transform.localScale = new Vector3(heightScale, heightScale, 1);
		*/

	}


	/*
	void Start () 
	{
		// set the desired aspect ratio (the values in this example are
		// hard-coded for 16:9, but you could make them into public
		// variables instead so you can set them at design time)
		float targetaspect = 16.0f / 10.0f;
		
		// determine the game window's current aspect ratio
		float windowaspect = (float)Screen.width / (float)Screen.height;
		
		// current viewport height should be scaled by this amount
		float scaleheight = windowaspect / targetaspect;
		
		// obtain camera component so we can modify its viewport
		Camera camera = GetComponent<Camera>();
		
		// if scaled height is less than current height, add letterbox
		if (scaleheight < 1.0f)
		{  
			Rect rect = camera.rect;
			
			rect.width = 1.0f;
			rect.height = scaleheight;
			rect.x = 0;
			rect.y = (1.0f - scaleheight) / 2.0f;
			
			camera.rect = rect;
		}
		else // add pillarbox
		{
			float scalewidth = 1.0f / scaleheight;
			
			Rect rect = camera.rect;
			
			rect.width = scalewidth;
			rect.height = 1.0f;
			rect.x = (1.0f - scalewidth) / 2.0f;
			rect.y = 0;
			
			camera.rect = rect;
		}
	}
	*/

}
