using System;
using UnityEngine;

public class CatInformation : MonoBehaviour
{
    private int _pawsAlive;
    private bool _catDied;

    private void Awake()
    {
        _pawsAlive = 2;
    }

    public void PawDied()
    {
        _pawsAlive--;
    }

    private void FixedUpdate()
    {
        if (_pawsAlive < 1)
        {
            _catDied = true;
            Debug.Log("Boss defeated");
        }
    }
}
