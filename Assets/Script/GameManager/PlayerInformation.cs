using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInformation : MonoBehaviour
{
    public GameObject playersInformation;

    // Start is called before the first frame update
    void Start()
    {
        
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
