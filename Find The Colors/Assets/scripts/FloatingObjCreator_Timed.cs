using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FloatingObjCreator_Timed : MonoBehaviour {

	public GameObject obj;
	public Text targetName;
	public Image targetImage;
	
	public Text score;
	public Text hits;
	public Text miss;

	public AudioSource audioSource;
	AudioClip audioclip;

	int scoreNo = 0;
	int hitsNo = 0;
	int missNo = 0;
	int timeNo = 60;
	
	List<string> objectsCreated = new List<string> ();
	System.Random random = new System.Random();
	
	int objIndex = 0;

	private bool isTouchDevice = false;
	
	void Awake() {
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android) 
			isTouchDevice = true; 
		else
			isTouchDevice = false;

        if (StaticArrays.matchingType.Equals("objects"))
        {
            obj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            obj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        }

    }
	
	// Use this for initialization
	void Start ()
	{
        DisplayAds.DisplayInterstitial();
        StaticArrays.currentMode = "timed";
		StaticArrays.currentModeHighScore = PlayerPrefs.GetInt (StaticArrays.timedModeHighScoreKey,0);

		//score.text = scoreNo.ToString();
		//score.text = timeNo.ToString();
		hits.text = hitsNo.ToString();
		miss.text = missNo.ToString();
		
		for (int i = 0; i < 10; i++) 
		{
			CreateObject();
		}
		GenerateTarget();	
		StartCoroutine (UpdateTime());

	}
	
	void Update() 
	{
		bool clickDetected;
		Vector3 touchPosition;
		
		// Detect click and calculate touch position
		if (isTouchDevice) {
			clickDetected = (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began);
			touchPosition = Input.GetTouch(0).position;
		} else {
			clickDetected = (Input.GetMouseButtonDown(0));
			touchPosition = Input.mousePosition;
		}

		if (clickDetected) {
		//if (Input.GetMouseButtonDown (0)){			
			Ray ray = Camera.main.ScreenPointToRay(touchPosition);
			RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
			
			if(/*hit != null && */hit.collider != null)
			{	
				hit.transform.gameObject.GetComponent<FloatingObj>().SetAsTouched();
				hit.transform.gameObject.GetComponent<PolygonCollider2D>().enabled = true;
				//objName.text = hit.transform.gameObject.name.Replace("(Clone)", ""); 
				
				if(targetName.text == hit.transform.gameObject.name.Replace("(Clone)", ""))
				{
                    GameObject instance = Instantiate(Resources.Load("sparkles", typeof(ParticleSystem)), hit.transform.position,hit.transform.rotation) as GameObject;
                    scoreNo = scoreNo + 4;
                    hitsNo = hitsNo + 1;
					//score.text = scoreNo.ToString();
					hits.text = hitsNo.ToString();
					
					DestroyImmediate(hit.transform.gameObject);
					objectsCreated.RemoveAt(objIndex);
					targetName.text = "";
					targetImage.sprite = Resources.Load<Sprite>("dot");
					Invoke ("GenerateTarget",0.1F);
					CreateObject();

				}else{
					scoreNo = scoreNo - 1;
					//score.text = scoreNo.ToString();
					missNo = missNo + 1;
					miss.text = missNo.ToString();
				}
				
			}
			
		}

		/*
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.LoadLevel ("ModeMenu"); 
		}
		*/
		
	}
	
	void CreateObject()
	{
		Vector3 position = new Vector3(UnityEngine.Random.Range(-7.0F, 6.0F), UnityEngine.Random.Range(-5.0F, 5.0F), UnityEngine.Random.Range(-9.5F,-0.5F));
		
		int index = random.Next(0,StaticArrays.objectNames.Length);

        string color = StaticArrays.objectNames[index];

        if (StaticArrays.matchingType.Equals("objects"))
        {
            string[] colorObjects = StaticArrays.objectDictionary[StaticArrays.objectNames[index]];
            int objIndex = random.Next(0, colorObjects.Length);

            string colorObj = colorObjects[objIndex];
            obj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(color + "/" + colorObj);
            obj.name = color.ToUpper();
        }
        else
        {
            int shapeIndex = random.Next(0, 2);
            obj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("colors/" + color + StaticArrays.shapeNames[shapeIndex]);
            obj.name = color.ToUpper();
        }       
		
		//DestroyImmediate(obj.GetComponent<PolygonCollider2D>(),true);
		//obj.AddComponent<PolygonCollider2D>();
		Instantiate(obj, position, Quaternion.identity);
		
		objectsCreated.Add (color);	
		
	}
	
	void GenerateTarget()
	{
		int count = objectsCreated.Count;
		if (count > 0) {
			objIndex = random.Next (0, count);
            int shapeIndex = random.Next(0, 2);
            targetName.text = (objectsCreated [objIndex]).ToUpper();
            //targetImage.sprite = Resources.Load<Sprite>("colors/" + objectsCreated[objIndex] + StaticArrays.shapeNames[shapeIndex]);
            targetImage.sprite = Resources.Load<Sprite>("colors/" + objectsCreated[objIndex] + "c");
            StaticArrays.currentTarget = objectsCreated [objIndex];
			PlayTargetSound();
		}
	}

	IEnumerator UpdateTime()
	{
		while (true) {
			yield return new WaitForSeconds (1f);
			SetTimeChange ();
		}
	}

	void SetTimeChange(){
		timeNo = timeNo - 1;
		score.text = timeNo.ToString ();
        /*
        if(timeNo > 40)
        {
            score.color = Color.blue;
        }
        else */
        if (timeNo == 20)
        {
            score.color = Color.red;
        }

        if (timeNo == 0) 
		{
			StaticArrays.currentTime = 60;
			StaticArrays.currentHits = hitsNo;
			StaticArrays.currentMisses = missNo;
			StaticArrays.currentScore = scoreNo;
			//Application.LoadLevel(Application.loadedLevel);
			//Application.LoadLevel("ScoreMenu");
            SceneManager.LoadScene("ScoreMenu");
        }
	}

	void PlayTargetSound(){
		audioclip = Resources.Load<AudioClip>(StaticArrays.currentTarget);
		//Debug.Log (StaticArrays.currentTarget);
		PlaySound ();
	}
	
	void PlaySound()
	{
		audioSource.PlayOneShot(audioclip);
	}
}
