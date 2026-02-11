using UnityEngine;

public class ControlPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<FlyingEnemy>().controlReached = true;
            other.GetComponent<FlyingEnemy>().startReached = false;
        }
    }
}
