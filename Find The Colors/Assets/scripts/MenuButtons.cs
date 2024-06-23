using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour {

	//public AudioSource audioSource;

	bool muteSound = false;

	public Sprite soundOn;
	public Sprite soundOff;
	public Button musicButton;
	public GameObject quitCanvas;

	//GameObject edubuzzLogo;
	public AudioSource audioSource;
	public AudioClip audioclip;


	void Start () {
		/*
		edubuzzLogo = GameObject.Find("EduBuzzLogo");
		audioSource = edubuzzLogo.GetComponent<AudioSource> ();
		audioclip = (AudioClip) Resources.Load("click");
		*/

	try
		{
			AudioSource audio = GameObject.Find ("BackgroundMusic").GetComponent<AudioSource> ();
			if(audio.isPlaying){
				musicButton.image.overrideSprite = soundOn;
			}else{
				musicButton.image.overrideSprite = soundOff;
			}
		}catch(Exception  e)
		{
			//ignore
			Debug.Log(e.ToString());
		}
	
	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if(quitCanvas.activeInHierarchy == true){
				HideQuitDialog();
			}else{
				ShowQuitDialog ();
			}
			
		}
	}


	void PlayTargetSound(){
		audioclip = Resources.Load<AudioClip>(StaticArrays.currentTarget);
		PlaySound ();

	}

	void PlaySound()
	{
		if (!audioSource.isPlaying) {
			audioSource.PlayOneShot(audioclip);
		}
		//audioSource.PlayOneShot(audioclip);
	}


	public void ToggleMusic()
	{	
		audioclip = Resources.Load<AudioClip> ("click");
		PlaySound ();
		AudioSource audio = GameObject.Find ("BackgroundMusic").GetComponent<AudioSource> ();
		if(audio.isPlaying){
			muteSound = true;
		}else{
			muteSound = false;
		}
		
		if(muteSound)
		{
			//AudioListener.volume =  0.0f;
			musicButton.image.overrideSprite = soundOff;
			GameObject.Find("BackgroundMusic").GetComponent<AudioSource>().Pause() ;
		}
		else
		{
			//AudioListener.volume =  1.0f;
			musicButton.image.overrideSprite = soundOn;
			GameObject.Find("BackgroundMusic").GetComponent<AudioSource>().UnPause() ;
		}
	}

	public void LoadScene(string sceneName)
	{
		audioclip = Resources.Load<AudioClip> ("click");
		PlaySound ();
		if(sceneName.Equals("colors")){
            //StaticArrays.objectNames = StaticArrays.colorNames;
            //Application.LoadLevel("ModeMenu");

            StaticArrays.matchingType = sceneName;
            SceneManager.LoadScene("ModeMenu");
        }
        else if(sceneName.Equals("objects")){
            //StaticArrays.objectNames = StaticArrays.colorNames;
            //Application.LoadLevel("ModeMenu");

            StaticArrays.matchingType = sceneName;
            SceneManager.LoadScene("ModeMenu");
        }
        else{		
			//Application.LoadLevel(sceneName);
            SceneManager.LoadScene(sceneName);
        }
	}

	public void PlayAgain()
	{
		string sceneName = "FloatingColors_Relaxed";

		if (StaticArrays.currentMode == "relaxed")
        {
            sceneName = "FloatingColors_Relaxed";
        }			
		else if (StaticArrays.currentMode == "timed")
        {
            sceneName = "FloatingColors_Timed";
        }
        //Application.LoadLevel(sceneName);
        SceneManager.LoadScene(sceneName);
    }

    public void ShowQuitDialog()
	{
		audioclip = Resources.Load<AudioClip> ("click");
		PlaySound ();
		//Debug.Log ("success");
		quitCanvas.SetActive(true);
	}
	
	public void HideQuitDialog()
	{
		audioclip = Resources.Load<AudioClip> ("click");
		PlaySound ();
		quitCanvas.SetActive(false);
	}
	
	public void Quit()
	{
		audioclip = Resources.Load<AudioClip> ("click");
		PlaySound ();
		Application.Quit();
	}


}
