using System.Collections;
using System.Collections.Generic;

using Ink.Runtime;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    Interaction interaction;
    Customer customer;
    Story story;

    [SerializeField] Image currentCustomerImage;
    [SerializeField] Image customerDialogueCloud;
    [SerializeField] TextMeshProUGUI customerDialogueText;

    [SerializeField] UserChoice[] userChoices;

    public void InjectDependencies(Interaction interaction, Customer customer)
    {
        this.interaction = interaction;
        this.customer = customer;

        currentCustomerImage.sprite = this.customer.dialogueSprite;
        
        InitializeDialogue();
    }

    void InitializeDialogue()
    {
        if (customer.inkStory is null) return;

        story = new Story(customer.inkStory.text);

        customerDialogueCloud.gameObject.SetActive(true);
        customerDialogueText.text = story.ContinueMaximally();
        PopulateUserChoices();
    }

    void ContinueStory()
    {
        //ResetUserChoices();
        if (story.canContinue)
        {
            customerDialogueText.text = story.Continue();
            PopulateUserChoices();
        }
        else
        {
        }
    }
    void PopulateUserChoices()
    {
        for(int i = 0;i < userChoices.Length;i++)
        {
            if (i < story.currentChoices.Count)
            {
                userChoices[i].gameObject.SetActive(true);
                userChoices[i].SetUserChoiceText(story.currentChoices[i].text);
            }
            else
            {
                userChoices[i].gameObject.SetActive(false);
            }            
        }
    }
    
    void ResetUserChoices()
    {
        foreach(UserChoice userChoice in userChoices)
        {
            userChoice.gameObject.SetActive(false);
        }
    }

    public void ChooseChoice(int index)
    {
        story.ChooseChoiceIndex(index);
        ContinueStory();
    }
}
