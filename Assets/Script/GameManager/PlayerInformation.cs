using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInformation : MonoBehaviour
{
    public GameObject playersInformation;
    private PlayerController playerController;
    public Text deaths;
    public Text playerName;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        deaths.text = PlayerPrefs.GetInt("deaths").ToString();
        playerName.text = PlayerPrefs.GetString("PlayerName");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("PlayersInformation"))
            playersInformation.SetActive(true);
        else
            playersInformation.SetActive(false);
    }
}
