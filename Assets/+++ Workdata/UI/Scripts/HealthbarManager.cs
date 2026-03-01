using System;
using UnityEngine;

public class HealthbarManager : MonoBehaviour
{
    public GameObject player;

    private PlayerInformation _playerInformation;
    public int _health;

    public GameObject[] heartList = new GameObject[10];                                                                 // Alle Herzhaelften in Array

    private void Awake()
    {
        _playerInformation = player.GetComponent<PlayerInformation>();
    }

    private void FixedUpdate()
    {
        _health = PlayerPrefs.GetInt("Health");                                                                     // Holt sich health ueber PlayerPrefs
        
        for (int i = 0; i < 10; i++)                                                                                    // Alle Herzen aus
        {
            heartList[i].SetActive(false);
        }

        for (int i = 0; i < ((_health) / 5); i++)                                                                       // Alle Herzen je nach Health an; Health wird umgerechnet; 5 Health sind ein halbes Herz
        {
            heartList[i].SetActive(true);
        }
    }
}
