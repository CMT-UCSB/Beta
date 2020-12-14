using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameManager gm;

    public void OnStart()
    {
        if(gm.GetLevel() == 1)
        {
            SceneManager.LoadScene("Level 1");
        }

        //later..
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
