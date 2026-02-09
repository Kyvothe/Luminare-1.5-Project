using UnityEngine;

public class SpikesRise : MonoBehaviour
{
    private Animator _animator;
    BoxCollider2D _boxCollider2D;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + 0.3f);
            //gameObject.GetComponent<BoxCollider2D>().enabled = true;
            
            _animator.SetBool("Angry", true);
            
            Debug.Log("Hello");
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - 0.3f);
            //gameObject.GetComponent<BoxCollider2D>().enabled = false;
            
            _animator.SetBool("Angry", false);

        }
        
        
    }
}
