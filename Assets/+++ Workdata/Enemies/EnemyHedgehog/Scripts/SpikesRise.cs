using UnityEngine;

public class SpikesRise : MonoBehaviour
{
    private Animator _animator;
    BoxCollider2D _boxCollider2D;

    public bool _awake;
    
    public AudioSource Snore;
    public AudioSource Angy;
    
    private AudioSource _currentAudioSource;

    private bool _walkedOut = false;

    public int _random;

    public GameObject BuffIgel;

    private Vector2 _spawnPosition;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _awake = false;
        PlayAudio(Snore);

        _spawnPosition.x = gameObject.transform.position.x;
        _spawnPosition.y = gameObject.transform.position.y + 0.2f;
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
            _random = Random.Range(0, 100);

            if (_random > 95)
            {
                Instantiate(BuffIgel, _spawnPosition, Quaternion.identity);
                Destroy(gameObject); 
            }
            else
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + 0.3f);
                //gameObject.GetComponent<BoxCollider2D>().enabled = true;

                _animator.SetInteger("ActionId", 1);
                _animator.SetTrigger("ActionTrigger");

                _walkedOut = false;
                _awake = true;
                PlayAudio(Angy);
            }
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
            _awake = false;

            _walkedOut = true;
            PlayAudio(Snore);
        }
    }

    public void PlayAudio(AudioSource newAudio)
    {
        if (_currentAudioSource)
            _currentAudioSource.Stop();

        _currentAudioSource = newAudio;
        _currentAudioSource.Play();
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
