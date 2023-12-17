using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Random = UnityEngine.Random;

public class Checkout : MonoBehaviour
{
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

    [SerializeField] List<GameObject> interactions;
    GameObject interactionCurrent;
    Interaction interactionScript;

    public Transform navMeshDestination;
    public event Action<Checkout> customerLeft;  

    // Update is called once per frame
    void Update()
    {
        
    }
    int ChooseInteraction()
    {
        //so far I only roll for an int, later this method will have an actual way of choosing an interaction
        return Random.Range(0, interactions.Count);
    }
    public void StartInteraction()
    {
        interactionCurrent = Instantiate(interactions[0]);
        interactionCurrent.GetComponentInChildren<Interaction>().
                           InjectDependencies(CustomerCurrent,playerScript);

        playerScript.InteractionPressed -= StartInteraction;
        playerScript.DisableMovement();


        playerScript.InteractionPressed += HideInteraction;
    }
    public void HideInteraction()
    {
        interactionCurrent.SetActive(false);

        playerScript.Input.PlayerActionMap.MovementAction.Enable();
        playerScript.InteractionPressed -= HideInteraction;
        playerScript.InteractionPressed += ShowInteraction;
    }
    public void ShowInteraction()
    {
        interactionCurrent.SetActive(true);

        playerScript.DisableMovement();
        playerScript.InteractionPressed -= ShowInteraction;
        playerScript.InteractionPressed += HideInteraction;
    }
    public void InteractionFinished()
    {
        customerLeft?.Invoke(this);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch(interactionCurrent is null)
            {
                case true:
                    playerScript.InteractionPressed += StartInteraction;
                    break;
                case false:
                    playerScript.InteractionPressed += ShowInteraction;
                    break;

            }
                    
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerScript.InteractionPressed -= StartInteraction;
            playerScript.InteractionPressed -= ShowInteraction;
        }
    }
}
