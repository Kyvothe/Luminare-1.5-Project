using System;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public enum FlyState{ Chase, Attack, Patrol}

    public FlyState flyState;
    
    public float speed;
    private GameObject player;
    //public bool chase = false;
    public Transform startPos;
    public Transform controlPos;
    public bool startReached = false;
    public bool controlReached = true;
    public Animator anim;
    public bool InAttackRange;

    public float attackDelayTime;
    private float _lastAttackTime;
    public float LastAttackTime => _lastAttackTime;

    public bool _canAttack;
    public bool _playerDetected;
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

        if (flyState == FlyState.Chase)
        {
            Chase();
            RotateChase();
        }

        if (flyState == FlyState.Patrol)
        {
            if (startReached == false)
            {
                ReturnToStart();
                Rotate();
            }

            if (controlReached == false)
            {
                goToControl();
                Rotate();
            }
        }

        if (_lastAttackTime + attackDelayTime < Time.time)
        {
            if (_canAttack)
            {
                InitiateAttack();
            }
            else if (_playerDetected)
            {
                flyState = FlyState.Chase;
            }
        }
    }


    public void PlayerDetected(bool value)
    {
        _playerDetected = value;
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

    public void SetAttackInfos(bool value)
    {
        _canAttack = value;
    }
    
    public void InitiateAttack()
    {
        flyState = FlyState.Attack;
        _lastAttackTime = Time.time;
        anim.SetBool("Attack", true);
    }

    public void EndAttack()
    {
        anim.SetBool("Attack", false);

        flyState = FlyState.Patrol;
    }

    /*
    private void AttackStart()
    {
        InAttackRange = true;
        AnimationSetBool(10, true);
    }

    private void AnimationSetBool(int id, bool value)
    {
        anim.SetBool(id, value);
    }//*/
    
    private void Rotate()
    {
        if(startReached == false)
            SetRotation(180);        
        else 
            SetRotation(0);
    }

    private void RotateChase()
    {
        if(transform.position.x > player.transform.position.x)
            SetRotation(180);
        else 
            SetRotation(0);
    }

    private void SetRotation(int yRot)
    {
        transform.rotation = Quaternion.Euler(0, yRot, 0);

    }
    
}
