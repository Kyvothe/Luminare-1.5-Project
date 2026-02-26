using Unity.Cinemachine;
using UnityEngine;

public class CamPrioritySetter : MonoBehaviour
{
    public CinemachineCamera cam;

    public void SetPriority(int priority)
    {
        cam.Priority = priority;
    }
}
