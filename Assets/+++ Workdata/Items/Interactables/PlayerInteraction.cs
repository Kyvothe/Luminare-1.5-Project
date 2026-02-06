using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public List<Interactable> currentInteractables;
    public GameObject interactionIndicator;
    
    private PlayerController playerController;
    
    private bool _playerIsAttacking;
    
    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }
    
    private void FixedUpdate()
    {
        _playerIsAttacking = playerController.ReturnIsAttacking();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        _playerIsAttacking = gameObject.GetComponent<PlayerController>().ReturnIsAttacking();                           // Extra Zuweisung als Absicherung
        
        if (_playerIsAttacking) return;                                                                                 // Nur wenn Player nicht attakce,d weil sonst AttackCollider vom Player Items in der Schwer-Range selected
        
        if (other.GetComponent<Interactable>())                                                                         // Abfrage, ob es ein Interactable ist
        {
            currentInteractables.Add(other.GetComponent<Interactable>());                                           // Item wird Liste hinzugefügt
            //other.GetComponent<Interactable>().onSelect?.Invoke();
            currentInteractables[^1].onSelect?.Invoke();                                                                // Zuletzt hingefügtes Items wird markiert
            
            interactionIndicator.SetActive(true);                                                                       // Läpchen über Player geht und zeigt Möglichleit zum Interagieren an
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_playerIsAttacking) return;
        
        if (other.GetComponent<Interactable>())
        {
            currentInteractables.Remove(other.GetComponent<Interactable>());                                        // Item wird aus Liste entfernt
            other.GetComponent<Interactable>().onDeselect?.Invoke();                                                    // Item nicht mehr markiert
        }

        if (currentInteractables.Count < 1)                                                                             // Lämpchen aus wenn keine Items in Liste, also kein Interactable in der Nähe
        {
            interactionIndicator.SetActive(false);
        }
    }

    public void TryInteract()                                                                                           // Aufgerufen über PlayerController Interact()
    {
        if (currentInteractables.Count < 1) return;                                                                     // Nur wenn mind. 1 Item in der Liste

        if (!currentInteractables[0].Interact())
        {
            if (currentInteractables[0].reusable)                                                                       // Abfrage, ob Item als reusable in Inspector gestellt wurde
            {
                Interactable currentInteractable = currentInteractables[0];                                             // Wenn reusable, dann wird es aus Liste genommen und hinten wieder eingefügt
                currentInteractables.RemoveAt(0);
                currentInteractables.Add(currentInteractable);
            }
            else
            {
                currentInteractables[0].DeactivateInteractable();                                                       // Wenn nicht reusable, dann wird aus Liste entfernt und Deaktivert
                currentInteractables.RemoveAt(0);
            }
        }
        
        
    }
}