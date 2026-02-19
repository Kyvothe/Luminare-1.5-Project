using System;
using UnityEngine;

public class PawInformation : MonoBehaviour
{
    public int _health;

    public GameObject cat;
    private CatInformation _catInformation;

    private void Awake()
    {
        _health = 2;

        _catInformation = cat.GetComponent<CatInformation>();
    }

    public void SetDamage(int damage)
    {
        _health -= damage;

        _catInformation.TakesDamage();

        if (_health < 1)
        {
            _catInformation.PawDied();
            Destroy(gameObject);
        }
        
        //gameObject.GetComponent<SpriteColorChanger>().ColorObject();
    }
}
