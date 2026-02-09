using System;
using UnityEngine;

public class HealthbarManager : MonoBehaviour
{
    public GameObject player;

    private PlayerInformation _playerInformation;
    private int _health;

    private void Awake()
    {
        _playerInformation = player.GetComponent<PlayerInformation>();
    }

    private void FixedUpdate()
    {
        //_health = _playerInformation;
    }
}
