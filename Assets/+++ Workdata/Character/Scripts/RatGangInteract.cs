using System;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.Audio;

public class RatGangInteract : MonoBehaviour
{
    private bool _playerIn = false;
    private bool _skippedCutscene= false;

    public Collider2D coll;
    public Collider2D coll2;
    
    public GameObject player;
    public GameObject RatGang;
    public GameObject pawLeft;
    public GameObject pawRight;
    public GameObject cat;

    public GameObject Light1;
    public GameObject Light2;

    public GameObject skipButton;
    
    private Animator _animator;
    
    private PawBehaviour _pawBehaviour1;
    private PawBehaviour _pawBehaviour2;
    private CatInformation _catInformation;

    public UnityEvent StartFight;
    
    public GameObject bossMusic;
    public GameObject ratMusic;
    public GameObject uhOh;

    private void Awake()
    { 
        _animator = RatGang.GetComponent<Animator>();
        
        _pawBehaviour1 = pawLeft.GetComponent<PawBehaviour>();
        _pawBehaviour2 = pawRight.GetComponent<PawBehaviour>();
        _catInformation = cat.GetComponent<CatInformation>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))                                                                                 // Start "Cutscene" RatGang
        {
            _playerIn = true;
            
            player.GetComponent<PlayerController>().ToggleInput(true);
            
            _animator.SetInteger("ActionId", 10);
            _animator.SetTrigger("ActionTrigger");
            
            Light1.SetActive(true);
            Light2.SetActive(true);
            
            ratMusic.SetActive(true);
            
            skipButton.SetActive(true);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerIn = false;
        }
    }

    public bool ReturnPlayerIn()
    {
        return _playerIn;
    }

    public void StartFightAfterAnimation()                                                                              // Start Fight
    {
        if (_skippedCutscene) return;
        
        _pawBehaviour1.SetSartFight();
        _pawBehaviour2.SetSartFight();
        _catInformation.StartFight();

        StartFight.Invoke();
            
        Light1.SetActive(false);
        Light2.SetActive(false);
        
        Destroy(coll.gameObject);                                                                                       // Blocks und Trigger weg
        Destroy(coll2.gameObject);
        
        skipButton.SetActive(false);
        
        ratMusic.SetActive(false);
        StartCoroutine(DelayBossMusic());
    }
    
    IEnumerator DelayBossMusic()
    {
        uhOh.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        uhOh.SetActive(false);
        bossMusic.SetActive(true);
    }

    public void SkipCutscene()
    {
        StartFightAfterAnimation();
        _animator.SetTrigger("ActionTrigger");
        _animator.SetInteger("ActionId", 100);
        _skippedCutscene = true;
    }
}
