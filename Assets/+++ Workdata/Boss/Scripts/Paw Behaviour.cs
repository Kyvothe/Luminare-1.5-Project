using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class PawBehaviour : MonoBehaviour
{
    private Animator _anim;

    public int id;

    public int damage;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        
        StartCoroutine(TimedRandom(5f));
    }
    

    private IEnumerator TimedRandom(float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            Debug.Log("Time");
        
            id = Random.Range(0, 3);
        
            _anim.SetInteger("ActionId", id);
            _anim.SetTrigger("ActionTrigger");
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerInformation>().SetDamage(damage);
        }
    }
}
