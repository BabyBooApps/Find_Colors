using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Random : MonoBehaviour
{

    string theObject;
    GameObject prefab;
    //GameObject theBalloon;
    GameObject theDestroyable;
    public int ObjectCount = 40;
    //public AudioSource blastAudioSource;
    //public AudioClip balloonPop;
    //public AudioClip fruitPop;
    //public AudioClip rocketPop;

    float lowerY = -7F;
    float upperY = -50F;
    int sceneNo;

    // Use this for initialization
    void Start()
    {
        //blastAudioSource = GameObject.Find ("EduBuzzLogo").GetComponent<AudioSource> ();

        //sceneNo = UnityEngine.Random.Range (1,4);
        if (StaticArrays.random == null)
        {
            StaticArrays.random = new System.Random();
        }
        //sceneNo = StaticArrays.random.Next(1,7);
        //sceneNo = StaticArrays.random.
        sceneNo = 1;
        //Debug.Log (sceneNo);

        StartCoroutine(InstantiateOverTime(40, 15, sceneNo));
        Invoke("ReloadLevel", 12F);

    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (sceneNo == 1 || sceneNo == 4)
                handleBalloonClicks();
            else
                handleClicks();
        }

        //if (Input.GetKeyDown(KeyCode.Escape)) 
        //	Application.LoadLevel("menu"); 
    }
    int GenerateRand()
    {
        return StaticArrays.random.Next(1, 23);
    }
    void handleBalloonClicks()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (/*hit != null &&*/ hit.collider != null)
        {
            //blastAudioSource.PlayOneShot(balloonPop);
            theDestroyable = hit.transform.gameObject;
            //Invoke("DestroyObject",0.1F);		
            //DestroyNow(hit.transform.gameObject);		
            StartCoroutine(DestroyObject(theDestroyable, 0.1f));
        }
    }

    void DestroyNow(GameObject theDestroyable)
    {
        Destroy(theDestroyable);
    }

    void handleClicks()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (/*hit != null && */ hit.collider != null)
        {
            theDestroyable = hit.transform.gameObject;
            if (theObject == "Rocket")
            {
                //blastAudioSource.PlayOneShot(rocketPop);
                hit.transform.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
                hit.transform.gameObject.GetComponent<Animator>().enabled = true;
                hit.transform.gameObject.GetComponent<Rigidbody2D>().gravityScale = -4f;

            }
            else if (theObject == "Fall")
            {
                //blastAudioSource.PlayOneShot(fruitPop);
                hit.transform.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
                hit.transform.gameObject.GetComponent<Rigidbody2D>().gravityScale = 2f;

            }

            StartCoroutine(DestroyObject(theDestroyable, 5f));
            //Invoke("DestroyObject",5F);		

        }
    }

    IEnumerator DestroyObject(GameObject theDestroyable, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Destroy(theDestroyable);
        // Now do your thing here
    }

    IEnumerator InstantiateOverTime(int count, int frames, int sceneNo)
    {
        int itemsPerFrame = (int)Mathf.Floor(count / frames);
        int spawned = 0;
        switch (sceneNo)
        {
            case 1:
            case 4:
                //blastAudioSource.clip = Resources.Load<AudioClip>("BalloonPop");
                prefab = Resources.Load<GameObject>("balloon");
                while (spawned < 60)
                {
                    for (int i = 0; i < itemsPerFrame; i++)
                    //&& spawned < count
                    {
                        Vector3 position;
                        if (spawned < 8)
                            position = new Vector3(UnityEngine.Random.Range(-8.0F, 8.0F), UnityEngine.Random.Range(lowerY, -20F), UnityEngine.Random.Range(-9.5F, -0.5F));
                        else
                            position = new Vector3(UnityEngine.Random.Range(-8.0F, 8.0F), UnityEngine.Random.Range(lowerY, upperY), UnityEngine.Random.Range(-9.5F, -0.5F));
                        //int balloonNo = UnityEngine.Random.Range(1,10);
                        //prefab.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("balloon/" + balloonNo );
                        int index = StaticArrays.random.Next(1, StaticArrays.objectNames.Length);
                        prefab.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(StaticArrays.objectNames[index]);
                        (Instantiate(prefab, position, Quaternion.identity) as GameObject).transform.parent = gameObject.transform;

                        spawned++;
                    }
                    yield return new WaitForEndOfFrame();

                }
                break;

            case 2:
            case 5:
                prefab = Resources.Load<GameObject>("Rocket");
                while (spawned < 60)
                {
                    for (int i = 0; i < itemsPerFrame; i++)
                    {

                        Vector3 position = new Vector3(UnityEngine.Random.Range(-8.0F, 8.0F), UnityEngine.Random.Range(lowerY, upperY), UnityEngine.Random.Range(-9.5F, -0.5F));
                        //Instantiate(prefab, position, Quaternion.identity);	
                        int rocketNo = UnityEngine.Random.Range(1, 5);
                        prefab.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("rocket/" + rocketNo);
                        (Instantiate(prefab, position, Quaternion.identity) as GameObject).transform.parent = gameObject.transform;
                        theObject = prefab.name;

                        spawned++;
                    }
                    yield return new WaitForEndOfFrame();
                }
                break;

            case 3:
            case 6:
                prefab = Resources.Load<GameObject>("Fall");
                while (spawned < count)
                {
                    for (int i = 0; i < itemsPerFrame; i++)
                    {
                        prefab.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("fruit" + UnityEngine.Random.Range(1, 5));
                        DestroyImmediate(prefab.GetComponent<CircleCollider2D>(), true);
                        prefab.AddComponent<CircleCollider2D>();

                        Vector3 position = new Vector3(UnityEngine.Random.Range(-8.0F, 8.0F), UnityEngine.Random.Range(6F, 40F), UnityEngine.Random.Range(-9.5F, -0.5F));
                        //Instantiate(prefab, position, Quaternion.identity);
                        (Instantiate(prefab, position, Quaternion.identity) as GameObject).transform.parent = gameObject.transform;
                        theObject = prefab.name;

                        spawned++;
                    }
                    yield return new WaitForEndOfFrame();
                }
                break;


            default:
                prefab = Resources.Load<GameObject>("Rocket");
                while (spawned < 60)
                {
                    for (int i = 0; i < itemsPerFrame; i++)
                    {

                        Vector3 position = new Vector3(UnityEngine.Random.Range(-8.0F, 8.0F), UnityEngine.Random.Range(lowerY, upperY), UnityEngine.Random.Range(-9.5F, -0.5F));
                        (Instantiate(prefab, position, Quaternion.identity) as GameObject).transform.parent = gameObject.transform;
                        theObject = prefab.name;

                        spawned++;
                    }
                    yield return new WaitForEndOfFrame();
                }
                break;

        }

    }



    void ReloadLevel()
    {
        //DestroyImmediate(gameObject);
        //Application.LoadLevel(2);
        //StaticArrays.reload = true;
        //Application.LoadLevel(Application.loadedLevel);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
