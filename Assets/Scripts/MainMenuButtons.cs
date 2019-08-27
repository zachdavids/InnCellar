using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

/*
 * Main Menu Logic
 */

public class MainMenuButtons : MonoBehaviour
{
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
