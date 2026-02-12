using UnityEngine;

public class AttackDamage : MonoBehaviour
{
    public int clawDamage;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerInformation>().SetDamage(clawDamage);  
        }
    }
}
