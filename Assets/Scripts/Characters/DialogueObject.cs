using System.Collections;
using System.Collections.Generic;

using Microsoft.Unity.VisualStudio.Editor;

using UnityEngine;

public class DialogueObject : MonoBehaviour, IDialogueable
{
    #region IDialogueable members
    
    [SerializeField]
    Sprite dialogueSprite;
    Sprite IDialogueable.DialogueSprite
    {
        get => dialogueSprite;
        set => dialogueSprite = value;
    }
    
    [SerializeField]
    TextAsset inkStory;
    TextAsset IDialogueable.InkStory 
    {
        get => inkStory;
        set => inkStory = value;
    }

    #endregion

    [SerializeField] Image dialogueIndicator;

    [SerializeField] GameObject dialoguePrefab;
    DialogueManager dialogue;
    Player player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(player is null)
            {
                player = collision.gameObject.GetComponent<Player>();
            }
            if(dialogue is null)
            {
                CreateDialogue();
            }
            player.Input.PlayerActionMap.InteractionAction.Enable();
            player.InteractionPressed += ShowHideDialogue;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.InteractionPressed -= ShowHideDialogue;
            player.Input.PlayerActionMap.InteractionAction.Disable();
        }
    }

    void CreateDialogue()
    {
        Debug.Log("You don't directly set dialogue's parent, and the prefab does not have it's own canvas. if you don't see it, that may be why");
        dialogue = Instantiate(dialoguePrefab).GetComponentInChildren<DialogueManager>();
        dialogue.InjectDependencies(this);
        dialogue.gameObject.SetActive(false);
    }

    void ShowHideDialogue()
    {
        dialogue.gameObject.SetActive(!dialogue.gameObject.activeSelf);

        if (player.Input.PlayerActionMap.MovementAction.enabled)
        {
            player.Input.PlayerActionMap.MovementAction.Disable();
        }
        else
        {
            player.Input.PlayerActionMap.MovementAction.Enable();
        }
    }
}
