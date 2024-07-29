using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitializationScript : MonoBehaviour
{
    

    public void On_Play_Btn_Click()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
