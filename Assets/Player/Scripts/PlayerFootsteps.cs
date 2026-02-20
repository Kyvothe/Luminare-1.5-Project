using System.Collections;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public AudioClip footStepsGrass;
    public AudioClip footStepsStone;
    public AudioClip footStepsCarpet;
    
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
            if (PlayerController.Hash_MovementValue > 0.1f )//&& playerController._isGrounded)
            {
                AudioManager.instance.PlaySFX(footStepsGrass);
            }

            yield return new WaitForSeconds(0.35f);
        }
    }
    
}
