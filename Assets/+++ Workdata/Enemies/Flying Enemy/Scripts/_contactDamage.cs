using UnityEngine;

public class _contactDamage : MonoBehaviour
{
    [SerializeField] private int contactDamage;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerInformation>().SetDamage(contactDamage);
            
        }
    }
}
