using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent onInteract;
    public UnityEvent onSelect;
    public UnityEvent onDeselect;

    public bool reusable;
    public bool destroyAfterUse;
    public bool isBerryBush;
    public bool isDoor;

    private bool _alreadyInteracted;
 
    public bool Interact()                                                                                              // Aufgerufen über PlayerInteraction
    {
        if (GetComponent<BerryBush>() != null)
        {
            if (!GetComponent<BerryBush>().CheckIfInteractable())
            {
                return false;
            }
            else
            {
                reusable = false;
            }
        }
        
        onInteract?.Invoke();                                                                                           // Unity event für sämtliche Interactions
        _alreadyInteracted = true;

        if (destroyAfterUse)                                                                                            // Wenn Item im Inspectpr auf DestroyAfteruse gestellt, wird es zerstört 
        {
            Destroy(gameObject);
            return true;
        }

        return false;                                                                                                   // Zurück zu TryInteract() --> false bedeutet, dass Item noch da und dann ziwschen reusable und nicht reusable entschieden werden muss
    }

    public void DeactivateInteractable()                                                                                // Aufgerufen über TryInteract()
    {
        GetComponent<Collider2D>().enabled = false;                                                                     // Item kann nicht wieder erkannt werden
        onDeselect?.Invoke();                                                                                           // Markierung wird aufgehoben
    }

}