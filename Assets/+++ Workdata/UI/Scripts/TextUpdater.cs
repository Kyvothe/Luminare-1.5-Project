using UnityEngine;
using TMPro;
using System;

public class TextUpdater : MonoBehaviour
{
    public GameObject player;
   
    private PlayerZoneCheck playerZoneCheck;
    private int _itemCount;

    private void Awake()
    {
        playerZoneCheck = player.GetComponent<PlayerZoneCheck>();
    }

    private void FixedUpdate()
    {
        _itemCount = playerZoneCheck.ReturnItemCount();                                                                 // Zugriff auf item count über Methode mit Rückgabewert
        gameObject.GetComponent<TMP_Text>().text = $"x {_itemCount}/10";
    }
}