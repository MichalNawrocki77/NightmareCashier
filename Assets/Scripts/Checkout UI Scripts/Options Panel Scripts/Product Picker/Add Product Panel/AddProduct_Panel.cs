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
        }
    }
    float weight;
    /// <summary>
    /// Method that populates all the labels and image of the Panel to the product that is intended to be added
    /// </summary>
    /// <param name="product">Product that is to be added</param>
    public void InitializePanel(Product product)
    {
        productDisplayedImg.sprite = product.sprite;
        productNameLabel.text = product.name;
        productQuantityLabel.text = 0.ToString();
        SetWeightText(0);
    }
    /// <summary>
    /// Method that lets you update the Panel's weight label by only passing the integer. The method will format the text as follows $"{weight} kg"
    /// </summary>
    /// <param name="weight">weight in integers</param>
    void SetWeightText(int weight)
    {
        productWeightLabel.text = $"{weight} kg";
    }
    public void AddButtonClicked()
    {
        interaction.AddProductToInteraction(product);
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
