using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class FlyingObjCreator : MonoBehaviour {

	public Text objName;

	GameObject theDestroyable;
	GameObject prefab ;


	void Start () {

		if (StaticArrays.random == null)
		{
			StaticArrays.random = new System.Random();
		}
		if (StaticArrays.createdObjectsList == null) {
			StaticArrays.createdObjectsList = new List<string>();
		}

		StartCoroutine(InstantiateOverTime());
		Invoke("GenerateTarget",1f);

	}
	
	void Update()
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			handleObjectClick();					
		}		
		//if (Input.GetKeyDown(KeyCode.Escape)) 
		//	Application.LoadLevel("menu"); 
	}

	IEnumerator InstantiateOverTime()
	{
		prefab = Resources.Load<GameObject> ("flying");

		while (true) 
		{
			Vector3 position;
			position = new Vector3 (UnityEngine.Random.Range (-2.5F, 2.5F), -7F, UnityEngine.Random.Range (-9.5F, -0.5F));					
			int index = StaticArrays.random.Next (0, StaticArrays.objectNames.Length);
			string currentObjectName = StaticArrays.objectNames [index];
			StaticArrays.createdObjectsList.Add(currentObjectName);
			prefab.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> (currentObjectName);
			prefab.name = currentObjectName;
			DestroyImmediate(prefab.GetComponent<PolygonCollider2D>(),true);
			prefab.AddComponent<PolygonCollider2D>();
			(Instantiate (prefab, position, Quaternion.identity)as GameObject).transform.parent = gameObject.transform;				
			yield return new WaitForSeconds (1F);				
		}
	}

	void GenerateTarget()
	{
		int lengthOfList = StaticArrays.createdObjectsList.Count;
		if (lengthOfList < 3) {
			StaticArrays.currentIndex = StaticArrays.random.Next (0, lengthOfList-1);
		} else {
			StaticArrays.currentIndex = StaticArrays.random.Next (0, 2);
		}
		objName.text = StaticArrays.createdObjectsList [StaticArrays.currentIndex];
	}

	void handleObjectClick(){
		
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
		
		if(/*hit != null &&*/ hit.collider != null)
		{
			if(objName.text == hit.transform.gameObject.name.Replace("(Clone)", ""))
			{
				DestroyImmediate(hit.transform.gameObject);
				StaticArrays.createdObjectsList.RemoveAt(StaticArrays.currentIndex);
				objName.text = "";
				Invoke ("GenerateTarget",0.5F);
			}
			//blastAudioSource.PlayOneShot(balloonPop);
			//theDestroyable = hit.transform.gameObject;
			//StartCoroutine(DestroyObject(theDestroyable, 0.1f));
		}
	}

	IEnumerator DestroyObject(GameObject theDestroyable, float delayTime)
	{
		yield return new WaitForSeconds(delayTime);
		Destroy (theDestroyable);
		// Now do your thing here
	}

}
