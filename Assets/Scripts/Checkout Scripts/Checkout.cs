using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Checkout : MonoBehaviour
{
    Customer customerCurrent;
    [SerializeField] Player playerScript;

    [SerializeField] List<GameObject> interactions;
    GameObject interactionCurrent;
    Interaction interactionScript;

    // Start is called before the first frame update
    void Start()
    {
    }

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
                           InjectDependencies(customerCurrent,playerScript);

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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Customer"))
        {
            customerCurrent = collision.gameObject.GetComponent<Customer>();
            Debug.Log(customerCurrent.name);
        }
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
        if (collision.CompareTag("Customer"))
        {
            customerCurrent = null;
            Debug.Log("No Peta :(");
        }

        if (collision.CompareTag("Player"))
        {
            playerScript.InteractionPressed -= StartInteraction;
            playerScript.InteractionPressed -= ShowInteraction;
        }
    }
}
