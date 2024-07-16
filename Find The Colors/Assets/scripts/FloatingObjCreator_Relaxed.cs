using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FloatingObjCreator_Relaxed: MonoBehaviour {

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
	int timeNo = 0;

	/*
	string[] objectNames = new string[]{"arrow","circle","crescent","cross","cube","cuboid","ellipse","heart","hexagon","oval",
							"parallelogram","pentagon","pyramid","rectangle","rhombus","semicircle","sphere","square",
							"star","trapezium","triangle"};
	*/

	//string[] objectNames = new string[]{"circle","cube","cuboid","pyramid","rectangle","sphere","square","triangle"};

	List<string> objectsCreated = new List<string> ();
	System.Random random = new System.Random();

	int objIndex = 0;

	private bool isTouchDevice = false;

	void Awake() {
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android) 
			isTouchDevice = true; 
		else
			isTouchDevice = false;

        //obj  = Resources.Load("floatingObj") as GameObject;
        
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
        StaticArrays.currentMode = "relaxed";
		StaticArrays.currentModeHighScore = PlayerPrefs.GetInt (StaticArrays.relaxedModeHighScoreKey,0);

		//score.text = scoreNo.ToString();
		score.text = timeNo.ToString();
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
		
		// Detect clicks
		//if (Input.GetMouseButtonDown (0)) 
		//{
		if (clickDetected) {		
			Ray ray = Camera.main.ScreenPointToRay(touchPosition);
			RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

			if(/*hit != null && */hit.collider != null)
			{
				if(targetName.text == hit.transform.gameObject.name.Replace("(Clone)", ""))
				{
					//DontDestroyMusic.Instance.PlaySuccessClip();
					PlayTargetSound();
					GameObject instance = Instantiate(Resources.Load("sparkles", typeof(ParticleSystem)), hit.transform.position,hit.transform.rotation) as GameObject;
					scoreNo = scoreNo + 8;
					hitsNo = hitsNo + 1;
					//score.text = scoreNo.ToString();
					hits.text = hitsNo.ToString();

					DestroyImmediate(hit.transform.gameObject);
					objectsCreated.RemoveAt(objIndex);
					targetName.text = "";
					targetImage.color =  new Color(1.0f,1.0f,1.0f);
					//targetImage.sprite = Resources.Load<Sprite>("dot");
					if (hitsNo == 20) {
						StaticArrays.currentTime = timeNo;
						StaticArrays.currentHits = hitsNo;
						StaticArrays.currentMisses = missNo;
						StaticArrays.currentScore = scoreNo + Mathf.RoundToInt(1600/timeNo);
						//Application.LoadLevel(Application.loadedLevel);
						//Application.LoadLevel("ScoreMenu");
                        SceneManager.LoadScene("ScoreMenu");
					}else{
						Invoke ("GenerateTarget",1.0F);
						CreateObject();
					}
				}else{
					DontDestroyMusic.Instance.Play_Failed_Clip();
					scoreNo = scoreNo - 2;
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
		Vector3 position = new Vector3(UnityEngine.Random.Range(-4.0F, 7.0F), UnityEngine.Random.Range(-1.5F, 4.0F), UnityEngine.Random.Range(-9.5F,-0.5F));
		
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
            int shapeIndex = random.Next(0,2);
            obj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("colors/" + color + StaticArrays.shapeNames[shapeIndex]);
            obj.name = color.ToUpper();
        }             


        //string objToLoad = StaticArrays.objectNames[index].ToUpper();


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
            //targetImage.color =  new Color(1.0f,1.0f,1.0f);
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
		timeNo = timeNo + 1;
		score.text = timeNo.ToString ();
	}

	void PlayTargetSound(){
		audioclip = Resources.Load<AudioClip>(StaticArrays.currentTarget);
		PlaySound ();
	}
	
	void PlaySound()
	{
		audioSource.PlayOneShot(audioclip);
		

	}

}
