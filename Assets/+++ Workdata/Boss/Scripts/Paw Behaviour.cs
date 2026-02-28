using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class PawBehaviour : MonoBehaviour
{
    private Animator _anim;

    public int id;

    public bool isLeft;

    public bool hasHit;
    public bool startFight;
    private bool _startedFight = false;

    public int damage;

    public GameObject decoPawLeft;
    public GameObject decoPawRight;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        hasHit = false;
    }
    
    public void SetSartFight()
    {
        startFight = true;
        
        StartCoroutine(TimedRandom(5.1f));
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


            if (id == 1 || id == 2)
            {
                (isLeft? decoPawLeft : decoPawRight).SetActive(false);
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasHit) return;
        
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerInformation>().SetDamage(damage);
            Debug.Log("Hit");
            hasHit = true;
        }
        
        if (other.CompareTag("SpawnPoint"))
        {
            other.GetComponent<SpawnHealItem>().DropItem();
        }
    }

    public void MakePawVisible()
    {
        (isLeft? decoPawLeft : decoPawRight).SetActive(true);
    }

    public void ResetHit()
    {
        hasHit = false;
    }
}
