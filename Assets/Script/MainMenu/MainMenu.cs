using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame() {
        SceneManager.LoadScene(1);
    }

    public void Settings() {
        SceneManager.LoadScene(2);
    }

    public void Back() {
        SceneManager.LoadScene(0);
    }


    public void QuitGame() {
        Application.Quit();
    }

    public void ReplayGame() {
        SceneManager.LoadScene(1);
    }

    public void Menu() {
        SceneManager.LoadScene(0);
    }
}