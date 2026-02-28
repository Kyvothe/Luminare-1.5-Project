using UnityEngine;
using System.Collections;

public class ApproachSquirrelBehaviour : MonoBehaviour
{
    public static readonly int Hash_ActionTrigger = Animator.StringToHash("ActionTrigger");
    public static readonly int Hash_ActionId = Animator.StringToHash("ActionId");
    
    private Animator _animator;
    public GameObject newProjectile;

    public bool _dropsStuff = false;
    Coroutine _coroutine;

    private Vector2 _spawnPosition;
    
    [SerializeField] private AudioClip _throwSound;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _spawnPosition = transform.position;
        _spawnPosition.y = transform.position.y - 0.5f;
    }

    private void Update()
    {
        if (_dropsStuff && _coroutine == null)
        {
            _coroutine = StartCoroutine(Loop());
        }
        else if (!_dropsStuff && _coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    IEnumerator Loop()
    {
        while (true)
        {
            DropProjectile();
            AudioManager.instance.PlaySoundFXClip(_throwSound, transform, 1f);
            yield return new WaitForSeconds(2f);
        }
    }
    
    private void DropProjectile()
    {
        GameObject newprojectile = Instantiate(newProjectile);
        newprojectile.transform.position = _spawnPosition;
        
        _animator.SetTrigger(Hash_ActionTrigger);
        _animator.SetInteger(Hash_ActionId, 10);
    }

    public void SetDropsStuff(bool value)
    {
        _dropsStuff = value;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Entered Squirrel");
            SetDropsStuff(true);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SetDropsStuff(false);
        }
    }
}
