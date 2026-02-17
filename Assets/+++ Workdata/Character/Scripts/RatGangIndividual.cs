using UnityEngine;
using System;
using System.Collections;
using Random = System.Random;

public class RatGangIndividual : MonoBehaviour
{
    public static readonly int Hash_ActionId = Animator.StringToHash("ActionId");
    public static readonly int Hash_ActionTrigger = Animator.StringToHash("ActionTrigger");
    
    private Animator _anim;

    public GameObject Ratgang;
    private RatGangInteract _ratGangInteract;

    private bool _playerIn;
    private bool _isTalking = false;
    
    private float _random = 0f;

    private void Awake()
    {
        _ratGangInteract = Ratgang.GetComponent<RatGangInteract>();
    }

    private void FixedUpdate()
    {
        _playerIn = _ratGangInteract.ReturnPlayerIn();

        CheckForPlayer();
        
        if (_random == 2 )
        {
            _anim.SetInteger(Hash_ActionId, 1);
            _anim.SetTrigger(Hash_ActionTrigger);

            _random = 0;
        }
    }

    private void CheckForPlayer()
    {
        if (_playerIn)
        {
            _isTalking = true;                                                                                          // irgenwas nicht gerferneced!!!!!!!!!!!!!!!!!!!
            
            StartCoroutine(RandomSpecial());                                                                        // Coroutine auschalten????????????? Gilt für alle interact scripts!!!!!!!!!!!
        
            Debug.Log("Ey"); // Dialog einbauen
        }
        else if (!_playerIn)
        {
            _anim.SetInteger(Hash_ActionId, 0);

            _isTalking = false;
        }
    }

    private IEnumerator RandomSpecial()
    {
        while (_isTalking)
        {
            yield return new WaitForSeconds(2f);
            _random = UnityEngine.Random.Range(1,3);
        }
    }
}
