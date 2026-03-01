using System;
using UnityEngine;
using UnityEngine.Events;

public class CatInformation : MonoBehaviour
{
    public static readonly int Hash_ActionTrigger = Animator.StringToHash("ActionTrigger");
    public static readonly int Hash_ActionId = Animator.StringToHash("ActionId");
    
    public int _pawsAlive;

    public GameObject item;
    
    private bool _catDied;

    private Animator _animator;
    
    private Vector2 _spawnPosition;
    
    public UnityEvent OnDeath;
    
    public AudioClip _catHurt;
    
    public AudioClip _catDefeat;

    private void Awake()
    {
        _pawsAlive = 3;
        
        _animator = GetComponent<Animator>();

        _spawnPosition.x = -4.39f;
        _spawnPosition.y = 4.12f;
    }

    public void StartFight()                                                                                            // Start Idle Animation
    {
        _animator.SetTrigger(Hash_ActionTrigger);
        _animator.SetInteger(Hash_ActionId, 100);
    }
    
    public void PawDied()                                                                                               // Aufgerufen wenn eine Paw kein Health mehr hat
    {
        _pawsAlive--;
    }

    public void TakesDamage()                                                                                           // Katze nimmt Schaden
    {
        Debug.Log("Hurt");
        _animator.SetTrigger(Hash_ActionTrigger);
        _animator.SetInteger(Hash_ActionId, 10);
        AudioManager.instance.PlaySoundFXClip(_catHurt, transform, 1f);
    }

    private void FixedUpdate()
    {
        if (_pawsAlive <= 1 && !_catDied)                                                                               // Katze tot
        {
            _catDied = true;
            _animator.SetInteger(Hash_ActionId, 20);
            OnDeath.Invoke();
            AudioManager.instance.PlaySoundFXClip(_catDefeat, transform, 1f);
        }
    }

    public void SpawnHealthUpgrade()                                                                                    // Spawn HealthUpgrade Soggy Pizza   
    {
        Instantiate(item, _spawnPosition, Quaternion.identity);
    }
}
