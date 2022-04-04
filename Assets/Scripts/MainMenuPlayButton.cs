using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPlayButton : MonoBehaviour
{

    public void StartPlay()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Intro");
    }
}
