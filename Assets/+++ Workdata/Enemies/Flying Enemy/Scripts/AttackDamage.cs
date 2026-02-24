using System;
using System.Collections;
using System.Threading;
using UnityEngine;

public class AttackDamage : MonoBehaviour
{
    public int clawDamage;

    public GameObject chaseContainer;
    private BoxCollider2D _coll;

    private void Awake()
    {
        _coll = chaseContainer.GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerInformation>().SetDamage(clawDamage);  
            
            _coll.enabled = false;

            StartCoroutine(AttackTimer());
            Debug.Log("routine started");
        }
    }

    private IEnumerator AttackTimer()
    {
        yield return new WaitForSeconds(4f);
        Debug.Log("timer vorbei");
        _coll.enabled = true;
    }
}
