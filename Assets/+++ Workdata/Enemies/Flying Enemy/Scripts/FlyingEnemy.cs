using System;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public float speed;
    private GameObject player;
    public bool chase = false;
    public Transform startPos;
    public Transform controlPos;
    public bool startReached = false;
    public bool controlReached = true;
    
    private void Start()
    {
     player = GameObject.FindGameObjectWithTag("Player");   
    }

    private void OnEnable()
    {
        startReached = false;
        controlReached = true;
    }

    private void Update()
    {
        if (!player)
        {
            return;
        }

        if (chase == true)
        {
            Chase();
        }

        if (startReached == false)
        {
            ReturnToStart();
        }

        if (controlReached == false)
        {
            goToControl();
        }

       //else
       //{
         //  ReturnToStart();
       //} 
       
       if(chase == false)
           Rotate();
       
       if(chase == true)
           RotateChase();
    }
    

    private void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    public void ReturnToStart()
    {
        transform.position = Vector2.MoveTowards(transform.position, startPos.position, speed * Time.deltaTime);
    }
    
    private void goToControl()
    {
        transform.position = Vector2.MoveTowards(transform.position, controlPos.position, speed * Time.deltaTime);
    }
    
    
    private void Rotate()
    {
        if(startReached == false)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        else 
            transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void RotateChase()
    {
        if(transform.position.x > player.transform.position.x)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        else 
            transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
