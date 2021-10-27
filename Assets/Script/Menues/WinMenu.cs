using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class WinMenu : MonoBehaviour
{
     public Text deathCount;

    // Start is called before the first frame update
    void Start()
    {
        deathCount = GameObject.Find("Death").GetComponent<Text>();
        deathCount.text = "Death: " + PlayerPrefs.GetInt("deaths").ToString();
    }

    public void ReplayGame() {
        SceneManager.LoadScene(1);
        PlayerPrefs.SetInt("deaths", 0);
    }

    public void Menu() {
        SceneManager.LoadScene(0);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
