using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider volume;

    private void Start() {
        volume = GameObject.Find("VolumeSlider").GetComponent<Slider>();
    }

    private void Update() {
        AudioListener.volume = volume.value;
    }
    public void Back() {
        SceneManager.LoadScene(0);
    }
}
