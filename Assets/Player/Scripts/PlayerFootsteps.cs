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

    private string _currentFloor;
    
    private PlayerController playerController;
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        StartCoroutine(PlayFootSteps());
    }

    IEnumerator PlayFootSteps()
    {
        while (true)
        {
            if (PlayerController.Hash_MovementValue > 0.1f && playerController._isGrounded)
            {
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

            yield return new WaitForSeconds(0.35f);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
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
