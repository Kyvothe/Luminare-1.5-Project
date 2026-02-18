using UnityEngine;

public class BerryBush : MonoBehaviour
{
    public int healAmount;
    
    public PlayerInformation playerInformation;

    public void Heal()
    {
        playerInformation.SetHealth(healAmount);
    }
}
