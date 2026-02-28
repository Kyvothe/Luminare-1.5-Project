using Unity.Cinemachine;
using UnityEngine;

public class CamPrioritySetter : MonoBehaviour
{
    // jeweilige cam kann neue Priority zugewiesen werden
    
    public CinemachineCamera cam;

    public void SetPriority(int priority)
    {
        cam.Priority = priority;
    }
}
