using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class UserChoice : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI userChoiceText;
    [SerializeField] DialogueManager dialogueManager;
    public void SetUserChoiceText(string choiceText)
    {
        userChoiceText.text = choiceText;
    }

    public void ChoiceClicked(int index)
    {
        dialogueManager.ChooseChoice(index);
    }
}
