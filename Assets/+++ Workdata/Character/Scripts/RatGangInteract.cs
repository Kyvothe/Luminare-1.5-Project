using System;
using UnityEngine;
using UnityEngine.Events;

public class RatGangInteract : MonoBehaviour
{
    private bool _playerIn = false;

    public Collider2D coll;
    public Collider2D coll2;
    
    public GameObject player;
    public GameObject RatGang;
    public GameObject pawLeft;
    public GameObject pawRight;
    public GameObject cat;

    public GameObject Light1;
    public GameObject Light2;
    
    private Animator _animator;
    
    private PawBehaviour _pawBehaviour1;
    private PawBehaviour _pawBehaviour2;
    private CatInformation _catInformation;

    public UnityEvent StartFight;

    private void Awake()
    { 
        _animator = RatGang.GetComponent<Animator>();
        
        _pawBehaviour1 = pawLeft.GetComponent<PawBehaviour>();
        _pawBehaviour2 = pawRight.GetComponent<PawBehaviour>();
        _catInformation = cat.GetComponent<CatInformation>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerIn = true;
            
            player.GetComponent<PlayerController>().ToggleInput(true);
            
            _animator.SetInteger("ActionId", 10);
            _animator.SetTrigger("ActionTrigger");
            
            Light1.SetActive(true);
            Light2.SetActive(true);
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

    public void StartFightAfterAnimation()
    {
        _pawBehaviour1.SetSartFight();
        _pawBehaviour2.SetSartFight();
        _catInformation.StartFight();

        StartFight.Invoke();
            
        Light1.SetActive(false);
        Light2.SetActive(false);
        
        Destroy(coll.gameObject);
        Destroy(coll2.gameObject);
    }
}
