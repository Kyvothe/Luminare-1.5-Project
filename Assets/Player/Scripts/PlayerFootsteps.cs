using System;
using System.Collections;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public AudioClip footStepsLeaf;
    public AudioClip footStepsConcrete;
    public AudioClip footStepsCarpet;
    public AudioClip footStepsMetal;
    public AudioClip footStepsWood;
    private bool _playThisShit = true;

    private string _currentFloor;
    private Vector2 _currentSpeed;
    private bool _yesIsGrounded;
    
    private PlayerController playerController;
    
    private void Awake()
    {
        _currentFloor = "Leaf";
    }
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        StartCoroutine(PlayFootSteps());
    }

    private void FixedUpdate()
    {
        _currentSpeed = playerController.ReturnMovement();
        _yesIsGrounded = playerController.ReturnIsGrounded();
    }

    IEnumerator PlayFootSteps()
    {
        while (_playThisShit)
        {
            if (_currentSpeed.x != 0 && _yesIsGrounded)
            {
                Debug.Log("footsteps");
                if (_currentFloor == "Leaf")
                {
                    AudioManager.instance.PlaySFX(footStepsLeaf);
                }

                if (_currentFloor == "Concrete")
                {
                    AudioManager.instance.PlaySFX(footStepsConcrete);
                }
                
                if (_currentFloor == "Carpet")
                {
                    AudioManager.instance.PlaySFX(footStepsCarpet);
                }
                
                if (_currentFloor == "Metal")
                {
                    AudioManager.instance.PlaySFX(footStepsMetal);
                }
                
                if (_currentFloor == "Wood")
                {
                    AudioManager.instance.PlaySFX(footStepsWood);
                }
            }

            yield return new WaitForSeconds(0.2f);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("_currentFloor: " + _currentFloor + "");
        if (other.CompareTag("Leaf")) SetFloor("Leaf");
        if (other.CompareTag("Carpet")) SetFloor("Carpet");
        if (other.CompareTag("Wood")) SetFloor("Wood");
        if (other.CompareTag("Concrete")) SetFloor("Concrete");
        if (other.CompareTag("Metal")) SetFloor("Metal");
    }
    
    private void SetFloor(string floor)
    {
        _currentFloor = floor;
    }
}
