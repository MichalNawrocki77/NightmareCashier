using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Random = UnityEngine.Random;

public class Checkout : MonoBehaviour
{
    //Keep in mind that customerCurrent is set in Customer.AssignCustomerToCheckout()
    public Customer CustomerCurrent {
        get
        {
            return customerCurrent;
        }
        set
        {
            customerCurrent = value;
            //Na wypadek gdyby gracz stal przy kasie, do ktorej customer jest przypisywany
            //In case the player is near a checkout to which a customer is assigned
            if (isPlayerInCollider && value is not null)
            {
                playerScript.AssignInteractionAction(ShowInteraction);
            }
            if(customerCurrent is null)
            {
                customerLeft?.Invoke(this);
            }
        }
    }
    bool isPlayerInCollider;
    private Customer customerCurrent;
    [SerializeField] Player playerScript;

    [SerializeField] GameObject interaction;
    GameObject interactionCurrent;
    public Interaction InteractionScript { get; private set; }

    public Transform navMeshDestination;
    public event Action<Checkout> customerLeft;  
    public void StartInteraction()
    {
        interactionCurrent = Instantiate(interaction,transform);
        InteractionScript = interactionCurrent.GetComponentInChildren<Interaction>();
        InteractionScript.InjectDependencies(CustomerCurrent,playerScript, this);

        InteractionScript.IsVisible = false;
    }
    public void HideInteraction()
    {
        if(interactionCurrent is null)
        {
            return;
        }

        InteractionScript.IsVisible = false;

        playerScript.EnableMovement();
        playerScript.AssignInteractionAction(ShowInteraction);
    }
    public void ShowInteraction()
    {
        if(interactionCurrent is null)
        {
            return;
        }

        InteractionScript.IsVisible = true;

        playerScript.DisableMovement();
        playerScript.AssignInteractionAction(HideInteraction);
    }
    public void InteractionFinished()
    {
        customerLeft?.Invoke(this);
    }
    public void DestroyInteraction()
    {
        Destroy(interactionCurrent.gameObject);
        interactionCurrent = null;
        playerScript.DisableIntarctionAction();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerScript.AssignInteractionAction(ShowInteraction);
            isPlayerInCollider = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerScript.DisableIntarctionAction();
            isPlayerInCollider = false;
        }
    }
}
