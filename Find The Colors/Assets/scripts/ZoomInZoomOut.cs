using UnityEngine;
using System.Collections;

public class ZoomInZoomOut : MonoBehaviour {

	void Start () {
		StartCoroutine(Zoom());
	}

	void ZoomOut()
	{
		//iTween.ScaleTo(gameObject,new Vector3(0.5f,0.5f,0.5f),0.5f);
		iTween.ScaleTo(gameObject,  iTween.Hash("scale", new Vector3(0.5f,0.5f,0.5f), "time", 1f,"easetype",iTween.EaseType.easeInElastic));
	}

	void ZoomIn()
	{
		//iTween.ScaleFrom(gameObject,new Vector3(1f,1f,1f),0.5f);
		//iTween.ScaleFrom(gameObject,  iTween.Hash("scale", new Vector3(1f,1f,1f), "time", 0.5f,"easetype",iTween.EaseType.spring));
		iTween.ScaleTo(gameObject,  iTween.Hash("scale", new Vector3(1f,1f,1f), "time", 1f,"easetype",iTween.EaseType.easeOutElastic));
	}

	IEnumerator Zoom()
	{
		while (true) {
			ZoomOut ();
			Invoke ("ZoomIn", 1f);
			//ZoomIn();
			yield return new WaitForSeconds (2f);
		}
	}
}
