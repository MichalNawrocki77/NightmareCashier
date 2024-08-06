using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enums;

using UnityEngine;

public class InteractableProductShelf : ProductShelf
{
    [Tooltip("Do not change this value since a setter is attached to it. This serialization is for reading/debugging purposes only")]
    [SerializeField] int currentProduct;
    //CurrentProduct property is not public, because I only want the behaviour of a setter
    int CurrentProduct
    {
        get
        {
            return currentProduct;
        }
        set
        {
            currentProduct = value;
            EvaluateCurrentSprite(value);
        }
    }

    [SerializeField] int maxProduct;

    [Tooltip("The higher the index, the more product this shelf is supposed to have (index 0 -> 0%, index 1 -> 0%-33% and so on)")]
    [SerializeField] Sprite[] sprites;
    SpriteRenderer spriteRenderer;

    Player player;

    protected override void Start()
    {
        base.Start();

        CurrentProduct = maxProduct;

        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        if (player is null) Debug.LogError($"{gameObject.name} did not find player");
    }

    /// <summary>
    /// Gets the desired amount of specified ProductType. This does not throw strikes, when the amount is too big, it simply return the amount a customer could actually get, Check if the amount is actually correct to give a player a strike
    /// </summary>
    /// <param name="requestedProduct">Type of product desired</param>
    /// <param name="amount">desired amount</param>
    /// <returns>The number of products the customer could get, if the shelf did not have enough product, then all the product customer encountered is returned</returns>
    public override int GetProductFromShelf(ProductType requestedProduct, int amount)
    {
        base.GetProductFromShelf(requestedProduct, amount);

        if(amount > CurrentProduct)
        {
            int productRecieved = CurrentProduct;
            CurrentProduct = 0;
            return productRecieved;
        }
        
        CurrentProduct -= amount;
        return amount;
    }

    private void EvaluateCurrentSprite(int value)
    {
        if(value > (2 * maxProduct /3))
        {
            spriteRenderer.sprite = sprites[3];
        }
        else if(value > (1 * maxProduct / 3))
        {
            spriteRenderer.sprite = sprites[2];
        }
        else if(value > 0)
        {
            spriteRenderer.sprite = sprites[1];
        }
        else if(value == 0)
        {
            spriteRenderer.sprite = sprites[0];
        }
        else
        {
            throw new Exception($"InteractableProductShelf encountered wrong value for it's currentProduct amount. The value is : {value}");
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.CompareTag("Player"))
        {
            player.AssignInteractionAction(RefillShelf);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.UnassignIntarctionAction();
        }
    }

    void RefillShelf()
    {
        if (player.currentlyHeldBox is null) return;

        if (player.currentlyHeldBox != this.product)
        {
            ShowWrongProductIndicator();
            return;
        }

        this.CurrentProduct = this.maxProduct;
    }

    private void ShowWrongProductIndicator()
    {
        Debug.LogWarning("ShowWrongProductIndicator() has not been implemented");
    }
}
