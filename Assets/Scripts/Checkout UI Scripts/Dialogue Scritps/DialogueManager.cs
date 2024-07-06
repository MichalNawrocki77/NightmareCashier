using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    Interaction interaction;
    Customer customer;

    [SerializeField] Image currentCustomerImage;
    [SerializeField] Image customerDialogueCloud;
    [SerializeField] TextMeshProUGUI customerDialogText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InjectDependencies(Interaction interaction, Customer customer)
    {
        this.interaction = interaction;
        this.customer = customer;

        currentCustomerImage.sprite = this.customer.dialogueSprite;
    }
}
