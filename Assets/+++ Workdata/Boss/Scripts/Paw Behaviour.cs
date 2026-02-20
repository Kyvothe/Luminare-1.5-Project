using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class PawBehaviour : MonoBehaviour
{
    private Animator _anim;

    public int id;

    public bool isLeft;

    public int damage;

    public GameObject decoPawLeft;
    public GameObject decoPawRight;

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


            if (id == 1 || id == 2)
            {
                (isLeft? decoPawLeft : decoPawRight).SetActive(false);
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerInformation>().SetDamage(damage);
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
}
