using System;
using UnityEngine;

public class HealthbarManager : MonoBehaviour
{
    public GameObject player;

    private PlayerInformation _playerInformation;
    public int _health;

    public GameObject[] heartList = new GameObject[8];

    private void Awake()
    {
        _playerInformation = player.GetComponent<PlayerInformation>();
    }

    private void FixedUpdate()
    {
        _health = _playerInformation.ReturnHealth();
        
        for (int i = 0; i < 8; i++)
        {
            heartList[i].SetActive(false);
        }

        for (int i = 0; i < ((_health) / 5); i++)
        {
            heartList[i].SetActive(true);
        }
        
    }
}
