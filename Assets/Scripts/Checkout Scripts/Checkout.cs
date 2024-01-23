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
            if(customerCurrent is null)
            {
                customerLeft?.Invoke(this);
            }
        }
    }
    private Customer customerCurrent;
    [SerializeField] Player playerScript;

    [SerializeField] GameObject interaction;
    GameObject interactionCurrent;
    public Interaction InteractionScript { get; private set; }

    public Transform navMeshDestination;
    public event Action<Checkout> customerLeft;  

    // Update is called once per frame
    void Update()
    {
        
    }
    int ChooseInteraction()
    {
        //so far I only roll for an int, later this method will have an actual way of choosing an interaction

        //Use this method as a means of checking products of the customer to determine which products will throw problems;

        //return Random.Range(0, interaction.Count);
        return 0;
    }
    public void StartInteraction()
    {
        interactionCurrent = Instantiate(interaction,transform);
        InteractionScript = interactionCurrent.GetComponentInChildren<Interaction>();
        InteractionScript.InjectDependencies(CustomerCurrent,playerScript);

        InteractionScript.IsVisible = false;
    }
    public void HideInteraction()
    {
        InteractionScript.IsVisible = false;

        playerScript.Input.PlayerActionMap.MovementAction.Enable();
        playerScript.InteractionPressed -= HideInteraction;
        playerScript.InteractionPressed += ShowInteraction;
    }
    public void ShowInteraction()
    {
        InteractionScript.IsVisible = true;

        playerScript.Input.PlayerActionMap.MovementAction.Disable();
        playerScript.InteractionPressed -= ShowInteraction;
        playerScript.InteractionPressed += HideInteraction;
    }
    public void InteractionFinished()
    {
        customerLeft?.Invoke(this);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && interactionCurrent is not null)
        {
            playerScript.InteractionPressed += ShowInteraction;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerScript.InteractionPressed -= ShowInteraction;
        }
    }
}
