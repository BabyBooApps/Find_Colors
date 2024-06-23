using UnityEngine;
using System.Collections;

public class BackgroundResize : MonoBehaviour {

    //public GameObject quitCanvas;

    Camera cam;

    void Start () {
        cam = Camera.main;
        Resize ();
	}	

	void Resize()
	{
		SpriteRenderer sr=GetComponent<SpriteRenderer>();
		if(sr==null) return;

        float aspect = (float)Screen.width / (float)Screen.height;

        float worldScreenHeight = 2f * cam.orthographicSize;
        float worldScreenWidth = worldScreenHeight * cam.aspect;
        //float worldScreenWidth = 2f * cam.orthographicSize * aspect;

        //float worldScreenHeight = Camera.main.orthographicSize * 2;
		//float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
		
		transform.localScale = new Vector3(worldScreenWidth + 10f / sr.sprite.bounds.size.x,
											worldScreenHeight / sr.sprite.bounds.size.y, 1);		
	}
	
}
