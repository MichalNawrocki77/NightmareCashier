using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class AddProduct_Panel : MonoBehaviour
{
    [SerializeField] Interaction interaction;

    Product product;
    [SerializeField] Image productDisplayedImg;
    [SerializeField] TextMeshProUGUI productNameLabel;
    [SerializeField] TextMeshProUGUI productQuantityLabel;
    [SerializeField] TextMeshProUGUI productWeightLabel;

    int quantity;
    public int Quantity
    {
        get
        {
            return quantity;
        }
        private set
        {
            quantity = value;
            productQuantityLabel.text = quantity.ToString();
            SetWeightText(Quantity * product.weight);
        }
    }
    /// <summary>
    /// Method that populates all the labels and image of the Panel to the product that is intended to be added
    /// </summary>
    /// <param name="product">Product that is to be added</param>
    public void InitializePanel(Product product)
    {
        this.product = product;
        productDisplayedImg.sprite = product.sprite;
        productNameLabel.text = product.name;
        Quantity = 0;
        SetWeightText(0);
    }
    /// <summary>
    /// Method that lets you update the Panel's weight label by only passing the integer. The method will format the text as follows $"{weight} kg"
    /// </summary>
    /// <param name="weight">weight in integers</param>
    void SetWeightText(float weight)
    {
        productWeightLabel.text = $"{weight.ToString("#,##")} kg";
    }
    public void AddButtonClicked()
    {
        for(int i = 0;i<Quantity;i++)
        {
            interaction.AddProductToInteraction(product);
        }  
        gameObject.SetActive(false);
    }
    public void PlusButtonClicked()
    {
        Quantity++;
    }
    public void MinusButtonClicked()
    {
        Quantity--;
    }
}
