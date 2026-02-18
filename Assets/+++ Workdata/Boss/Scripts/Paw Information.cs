using System;
using UnityEngine;

public class PawInformation : MonoBehaviour
{
    public int _health;

    public GameObject cat;

    private void Awake()
    {
        _health = 20;
    }

    public void SetDamage(int damage)
    {
        _health -= damage;

        if (_health < 1)
        {
            cat.GetComponent<CatInformation>().PawDied();
        }
        
        gameObject.GetComponent<SpriteColorChanger>().ColorObject();
    }
}
