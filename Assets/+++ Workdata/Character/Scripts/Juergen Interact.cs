using System;
using UnityEngine;

public class JuergenInteract : MonoBehaviour
{
    public static readonly int Hash_ActionId = Animator.StringToHash("ActionId");
    public static readonly int Hash_ActionTrigger = Animator.StringToHash("ActionTrigger");
    
    private Animator _anim;

    public GameObject player;
    private PlayerZoneCheck _playerZoneCheck;

    private bool _gotAllItems;

    private void Awake()
    {
        _anim = gameObject.GetComponent<Animator>();

        _playerZoneCheck = player.GetComponent<PlayerZoneCheck>();
    }

    private void FixedUpdate()
    {
        _gotAllItems = _playerZoneCheck.ReturnGotItems();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Hi");
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _anim.SetInteger(Hash_ActionId, 0);
        }
    }

    public void StartDialogue()
    { 
        _anim.SetInteger(Hash_ActionId, 1);
        _anim.SetTrigger(Hash_ActionTrigger);

        if (_gotAllItems)
        {
            Debug.Log("nom nom"); // Dialog einbauen belohnung
            player.GetComponent<PlayerController>().SetCanDoubleJump(true);
        }
        else
        {
            Debug.Log("hello there"); // Dialog einbauen start der quest
            player.GetComponent<PlayerController>().SetCanBigJump(true);

        }
    }
}
