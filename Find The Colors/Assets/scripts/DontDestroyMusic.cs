using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DontDestroyMusic : MonoBehaviour
{

    //public GameObject background;

    //public AudioSource bgMusic;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        //DontDestroyOnLoad(bgMusic);
        //DontDestroyOnLoad(background);
        //DisplayAds.RequestBanner();
        //Application.LoadLevel("MainMenu");
        SceneManager.LoadScene("MainMenu");
    }
}
