using System.Collections;
using System.Collections.Generic;

using Ink.Runtime;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    IDialogueable dialogueObject;
    Story story;

    [SerializeField] Image dialogueableObjectImage;
    [SerializeField] Image customerDialogueCloud;
    [SerializeField] TextMeshProUGUI customerDialogueText;

    [SerializeField] UserChoice[] userChoices;

    public void InjectDependencies(IDialogueable dialogueableObject)
    {
        dialogueObject = dialogueableObject;
        dialogueableObjectImage.sprite = dialogueObject.DialogueSprite;
        
        InitializeDialogue();
    }

    void InitializeDialogue()
    {
        if (dialogueObject.InkStory is null) return;

        story = new Story(dialogueObject.InkStory.text);

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
    //ResetUserChocies() probably should be deleted
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
