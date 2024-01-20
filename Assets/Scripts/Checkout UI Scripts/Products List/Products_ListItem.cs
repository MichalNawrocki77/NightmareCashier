using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using TMPro;

using UnityEditor.UIElements;

using UnityEngine;

public class Products_ListItem : MonoBehaviour
{
    public Product product;
    int quantity;
    [SerializeField] Image listItemImageComponent;
    [SerializeField] TextMeshProUGUI productNameLabel;
    [SerializeField] TextMeshProUGUI quantityLabel;
    [SerializeField] TextMeshProUGUI productPriceLabel;
    [SerializeField] TextMeshProUGUI fullPriceLabel;
    [SerializeField] TextMeshProUGUI productWeightLabel;
    [SerializeField] TextMeshProUGUI fullWeightLabel;

    private void Awake()
    {
        //When a new ListItem is instantiated quantity is supposed to be 1. The reason it is in awake and not in start is that the initial adding of ListItems in ProductList is called alongside instantiate(), and since awake only awake is called when instantiate is called and not start, the quantity has to be set in awake (so that when ListItems, adds another product of same type, the quantity is properly set to 1, and not the int default value of 0)
        quantity = 1;
    }
    private void Start()
    {
        listItemImageComponent.sprite = product.sprite;
        productNameLabel.text = product.name;
        
        SetQuantityLabelText(quantity);

        SetPriceLabelText(product.price);
        SetProductWeightLabelText(product.weight);

        SetFullPriceLabelText(product.price * quantity);
        SetFullWeightLabelText(product.weight * quantity);
    }

    #region labels text setters
    /// <summary>
    /// method to correctly set the quantity label text by just passing an int. It automatically formats the text to $"{quantity} pcs"
    /// </summary>
    /// <param name="quantity">quantity as int</param>
    void SetQuantityLabelText(int quantity)
    {
        quantityLabel.text = $"{quantity} pcs";
    }
    /// <summary>
    /// method to correctly set the products price label text by just passing an float. It automatically formats the text to $"{price} pcs"
    /// </summary>
    /// <param name="price">full price as float</param>
    void SetPriceLabelText(float price)
    {
        productPriceLabel.text = $"{price}$";
    }
    /// <summary>
    /// method to correctly set the full price label text by just passing an int. It automatically formats the text to $"{price}$"
    /// </summary>
    /// <param name="price">price as float</param>
    void SetFullPriceLabelText(float price)
    {
        fullPriceLabel.text = $"{price}$";
    }
    /// <summary>
    /// method to correctly set the product weight label text by just passing a float. It automatically formats the text to $"{weight} kg"
    /// </summary>
    /// <param name="weight">quantity as float</param>
    void SetProductWeightLabelText(float weight)
    {
        productWeightLabel.text = $"{weight}kg";
    }
    /// <summary>
    /// method to correctly set the full weight label text by just passing a float. It automatically formats the text to $"{weight} kg"
    /// </summary>
    /// <param name="weight">weight as float</param>
    void SetFullWeightLabelText(float weight)
    {
        fullWeightLabel.text = $"{weight}kg";
    }
    #endregion
    public void UpdateQuantity(int newQuantity=0)
    {
        quantity = newQuantity;
        SetQuantityLabelText(quantity);
        SetFullPriceLabelText(product.price * quantity);
        SetFullWeightLabelText(product.weight * quantity);
    }
    public void IncrementQuantity()
    {
        UpdateQuantity(++quantity);
    }

}
