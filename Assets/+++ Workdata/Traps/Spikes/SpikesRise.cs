using UnityEngine;

public class SpikesRise : MonoBehaviour
{
    
    BoxCollider2D _boxCollider2D;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + 0.3f);
            //gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - 0.3f);
            //gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
