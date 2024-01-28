using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour
{
    public GameObject canvas;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Controls()
    {
        canvas.SetActive(false);
    }

    public void Back()
    {
        canvas.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
