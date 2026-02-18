using UnityEngine;

public class SpikesRise : MonoBehaviour
{
    private Animator _animator;
    BoxCollider2D _boxCollider2D;

    private bool _walkedOut = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_walkedOut)
        {
            _animator.SetBool("Awake", false);
            _animator.SetInteger("ActionId", 0);
        }
    }
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + 0.3f);
            //gameObject.GetComponent<BoxCollider2D>().enabled = true;
            
            _animator.SetInteger("ActionId", 1);
            _animator.SetTrigger("ActionTrigger");
            
            _walkedOut = false;
        }
    }
    
    public void PlayAwake()
    { 
        _animator.SetBool("Awake", true);
        _animator.SetInteger("ActionId", 0);
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - 0.3f);
            //gameObject.GetComponent<BoxCollider2D>().enabled = false;
            
            _animator.SetBool("Awake", false);

            _walkedOut = true;

        }
    }
    
    
    
    
    /*
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + 0.3f);
            //gameObject.GetComponent<BoxCollider2D>().enabled = true;
            
            _animator.SetInteger("ActionId", 1);
        }
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _animator.SetBool("Awake", true);
            _animator.SetInteger("ActionId", 0);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - 0.3f);
            //gameObject.GetComponent<BoxCollider2D>().enabled = false;
            
            _animator.SetBool("Awake", false);

        }
    }
    
    */
}
