using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class OpenGameOverFailSafe : MonoBehaviour
{
    // will ensure the gameover screen opens in case the death animation gets overwritten

    public GameObject MenuManagerInGame;

    public int _health;
    private PlayerInformation _playerInformation;

    private void Awake()
    {
        _playerInformation = GetComponent<PlayerInformation>();
    }

    private void FixedUpdate()
    {
        _health = _playerInformation.ReturnHealth();

        if (_health <= 0)
        {
            StartCoroutine(ReallyGameOver());
        }
    }

    private IEnumerator ReallyGameOver()
    {
        yield return new WaitForSeconds(4f);                                                                            // force open GameOverScreen nach 4 Sekunden nach 0 health
        MenuManagerInGame.GetComponent<OpenDialogueInGame>().OpenGameOverScreen();
    }
}
