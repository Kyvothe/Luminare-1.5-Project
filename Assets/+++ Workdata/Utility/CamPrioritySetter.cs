using Unity.Cinemachine;
using UnityEngine;

public class CamPrioritySetter : MonoBehaviour
{
    public CinemachineCamera camera;

    public void SetPriority(int priority)
    {
        camera.Priority = priority;
    }
}
