using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public SceneFader sceneFader;
    public void StartGame()
    {
        sceneFader.FadeTo("GameScene");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
