using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class DontDestroyMusic : MonoBehaviour
{

    public static DontDestroyMusic Instance;
    public AudioSource[] AudioSources;
    public AudioSource GameSounds_Source;
    public List<AudioClip> SuccessClips;
    public List<AudioClip> FailedClips;
    //public GameObject background;

    //public AudioSource bgMusic;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        //DontDestroyOnLoad(bgMusic);
        //DontDestroyOnLoad(background);
        //DisplayAds.RequestBanner();
        //Application.LoadLevel("MainMenu");
        //SceneManager.LoadScene("MainMenu");
    }

    private void Start()
    {
        Get_AudioSource();
    }

    public void Get_AudioSource()
    {
        AudioSources =this.GetComponents<AudioSource>() as AudioSource[];
        GameSounds_Source = AudioSources[1];
    }

    public void On_Play_Btn_Click()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Play_Button_Sound_Clip(AudioClip clip)
    {
        GameSounds_Source.Stop();
        GameSounds_Source.clip = clip;
        GameSounds_Source.loop = false;
        GameSounds_Source.Play();
    }

    public void PlaySuccessClip()
    {
        AudioClip clip = SuccessClips.GetRandomElement();
        Play_Button_Sound_Clip(clip);
    }

    public void Play_Failed_Clip()
    {
        AudioClip clip = FailedClips.GetRandomElement();
        Play_Button_Sound_Clip(clip);
    }
}
