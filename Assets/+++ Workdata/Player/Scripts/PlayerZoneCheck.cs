using UnityEngine;

public class PlayerZoneCheck : MonoBehaviour
{
    [SerializeField] private int itemCount = 0;
    public bool gotAllItems = false;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PickupItem"))
        {
            itemCount++;
            other.gameObject.SetActive(false);
            
            if (itemCount == 5)
            { 
                gotAllItems = true;
            }
        }
    }
}
