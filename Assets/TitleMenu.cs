using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    public void OnStart()
    {
        SceneManager.LoadScene("Intro Cutscene"); 
    }

    public void OnOptions()
    {
        //TODO
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
