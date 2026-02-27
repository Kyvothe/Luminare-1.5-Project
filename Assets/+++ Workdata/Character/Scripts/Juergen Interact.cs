using System;
using UnityEngine;
using UnityEngine.Events;

public class JuergenInteract : MonoBehaviour
{
    public static readonly int Hash_ActionId = Animator.StringToHash("ActionId");
    public static readonly int Hash_ActionTrigger = Animator.StringToHash("ActionTrigger");
    
    public UnityEvent onUpgrade;
    
    private Animator _anim;

    public GameObject player;
    private PlayerZoneCheck _playerZoneCheck;

    public GameObject dialogueFirst;
    public GameObject dialogueSecond;
    public GameObject dialogueUpgrade;
    private GameObject _dialogue;
    
    private bool _gotAllItems;
    private bool _walkedInFirstTime;

    private void Awake()
    {
        _anim = gameObject.GetComponent<Animator>();

        _playerZoneCheck = player.GetComponent<PlayerZoneCheck>();
        
        _walkedInFirstTime = true;
        
        _dialogue = dialogueFirst;
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
            
            _dialogue.SetActive(false);
            dialogueUpgrade.SetActive(false);
        }
    }

    public void StartDialogue()
    { 
        _anim.SetInteger(Hash_ActionId, 1);
        _anim.SetTrigger(Hash_ActionTrigger);
        
        _dialogue.SetActive(false);

        if (_gotAllItems)
        {
            player.GetComponent<PlayerController>().SetCanDoubleJump(true);
            player.GetComponent<PlayerController>().SetSock(true);
            
            _dialogue = dialogueUpgrade;
            _dialogue.SetActive(true);
            onUpgrade.Invoke();
        }
        
        if (_walkedInFirstTime)
        {
            player.GetComponent<PlayerController>().SetCanBigJump(true);
            
            _walkedInFirstTime = false;
            
            _dialogue = dialogueFirst;
            _dialogue.SetActive(true);

        }
        else if (!_walkedInFirstTime)
        {
            _dialogue = dialogueSecond;
            _dialogue.SetActive(true);
        }
    }
}
