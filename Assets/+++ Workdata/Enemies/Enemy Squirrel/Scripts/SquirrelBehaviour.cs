using UnityEngine;
using System.Collections;

public class SquirrelBehaviour : MonoBehaviour
{
    public static readonly int Hash_ActionTrigger = Animator.StringToHash("ActionTrigger");
    public static readonly int Hash_ActionId = Animator.StringToHash("ActionId");
    
    private Animator _animator;
    public GameObject newProjectile;

    public bool _dropsStuff;
    Coroutine _coroutine;

    private void Awake()
    {
        _dropsStuff = true;
        _animator = GetComponent<Animator>();
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
            yield return new WaitForSeconds(3f);
        }
    }
    
    private void DropProjectile()
    {
        GameObject newprojectile = Instantiate(newProjectile);
        newprojectile.transform.position = gameObject.transform.position;
        
        _animator.SetTrigger(Hash_ActionTrigger);
        _animator.SetInteger(Hash_ActionId, 10);
    }

    public void SetDropsStuff(bool value)
    {
        _dropsStuff = value;
    }

}
