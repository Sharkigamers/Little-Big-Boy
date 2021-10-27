using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TMP_InputField playerName;

    public void Start() {
        playerName =  GameObject.Find("InputField (TMP)").GetComponent<TMP_InputField>();
        playerName.text = "Player";
        PlayerPrefs.SetInt("deaths", 0);
    }

    public void PlayGame() {
        PlayerPrefs.SetString("PlayerName", playerName.text);
        SceneManager.LoadScene(1);
    }

    public void Settings() {
        SceneManager.LoadScene(3);
    }

    public void QuitGame() {
        Application.Quit();
    }
}