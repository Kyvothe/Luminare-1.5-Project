using UnityEngine;
using System;
using System.Collections;
using Random = System.Random;

public class RalphInteract : MonoBehaviour
{
    public static readonly int Hash_ActionId = Animator.StringToHash("ActionId");
    public static readonly int Hash_ActionTrigger = Animator.StringToHash("ActionTrigger");
    
    private Animator _anim;

    public GameObject player;
    private PlayerZoneCheck _playerZoneCheck;
    
    public GameObject dialogueFirst;
    public GameObject dialogueSecond;
    public GameObject dialogueUpgrade;
    private GameObject _dialogue;

    private bool _gotSunGlasses;
    private bool _walkedInFirstTime;

    private bool _isTalking = false;

    private float _random = 0f;

    private void Awake()
    {
        _anim = gameObject.GetComponent<Animator>();

        _playerZoneCheck = player.GetComponent<PlayerZoneCheck>();
        
        _walkedInFirstTime = true;
        
        _dialogue = dialogueFirst;
    }

    private void FixedUpdate()
    {
        _gotSunGlasses = _playerZoneCheck.ReturnSunGlasses();

        if (_random == 2 )
        {
            _anim.SetInteger(Hash_ActionId, 3);
            _anim.SetTrigger(Hash_ActionTrigger);

            _random = 0;
        }
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

            _isTalking = false;
            
            _dialogue.SetActive(false);
            dialogueUpgrade.SetActive(false);
            
            StopCoroutine(RandomSpecial());
        }
    }

    public void StartDialogue()
    { 
        _anim.SetInteger(Hash_ActionId, 1);
        _anim.SetTrigger(Hash_ActionTrigger);

        _isTalking = true;

        StartCoroutine(RandomSpecial());

        if (_gotSunGlasses)
        {
            Debug.Log("Whatever"); // Dialog einbauen belohnung
            player.GetComponent<PlayerController>().SetCanAttack(true);
            
            _dialogue = dialogueUpgrade;
            _dialogue.SetActive(true);
        }
        
        if (_walkedInFirstTime)
        {
            Debug.Log("I want sun glasses you worm"); // Dialog einbauen start der quest
            
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

    private IEnumerator RandomSpecial()
    {
        while (_isTalking)
        {
            yield return new WaitForSeconds(1.5f);
            _random = UnityEngine.Random.Range(1,3);
        }
    }
}

