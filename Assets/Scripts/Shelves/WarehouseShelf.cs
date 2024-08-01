using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarehouseShelf : MonoBehaviour
{
    [SerializeField] ProductSO product;
    [SerializeField] Player player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AssignCorrectPlayerAction();
        }
    }

    private void AssignCorrectPlayerAction()
    {
        if (player.currentlyHeldBox is null)
        {
            player.AssignInteractionAction(GetBoxWithProducts);
            return;
        }
        player.AssignInteractionAction(PutBoxDown);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player.DisableIntarctionAction();
    }
    void GetBoxWithProducts()
    {
        player.PickUpBox(product);

        AssignCorrectPlayerAction();
    }

    void PutBoxDown()
    {
        if(player.currentlyHeldBox != product)
        {
            ShowWrongProductIndicator();
            return;
        }
        player.PutDownbox();

        AssignCorrectPlayerAction();
    }

    private void ShowWrongProductIndicator()
    {
        Debug.Log("You try to put down the wrong product.");
    }
}
