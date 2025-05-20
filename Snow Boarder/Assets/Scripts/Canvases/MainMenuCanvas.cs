using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuCanvas : MonoBehaviour
{
    public void StartGame() => LevelLoader.Instance.LoadFirstLevel();

    public void LoadLevelByName()
    {

    }

    public void OpenSettings()
    {

    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
