using System;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEnterCam : MonoBehaviour
{
    public UnityEvent TriggerEnter;
    public UnityEvent TriggerExit;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerEnter.Invoke();
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerExit.Invoke();
        }
    }
}
