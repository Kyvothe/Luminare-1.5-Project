using UnityEngine;

public class StartingPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<FlyingEnemy>().startReached = true;
            other.GetComponent<FlyingEnemy>().controlReached = false;
        }
    }
}