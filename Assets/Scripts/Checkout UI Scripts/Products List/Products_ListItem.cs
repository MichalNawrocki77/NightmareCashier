using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using TMPro;

using UnityEngine;
using System;

public class Products_ListItem : MonoBehaviour
{
    public Product product;
    [HideInInspector]
    public ModifyProduct_Panel modifyProductPanel;

    public int Quantity
    {
        get
        {
            return quantity;
        }
        private set
        {
            quantity = value;
            SetQuantityLabelText(quantity);
            FullWeight = product.weight * quantity;
            FullPrice = product.price * quantity;
        }
    }
    int quantity;
    
    
    public float FullPrice
    {
        get
        {
            return fullPrice;
        }
        private set
        {
            fullPrice = GetFloatWIthTwoDecimalPlaces(value);
            SetFullPriceLabelText(fullPrice);
        }
    }
    float fullPrice;
    
    public float FullWeight
    {
        get
        {
            return fullWeight;
        }
        set
        {
            fullWeight = GetFloatWIthTwoDecimalPlaces(value);
            SetFullWeightLabelText(FullWeight);
        }
    }
    float fullWeight;

    [SerializeField] Image listItemImageComponent;
    [SerializeField] TextMeshProUGUI productNameLabel;
    [SerializeField] TextMeshProUGUI quantityLabel;
    [SerializeField] TextMeshProUGUI productPriceLabel;
    [SerializeField] TextMeshProUGUI fullPriceLabel;
    [SerializeField] TextMeshProUGUI productWeightLabel;
    [SerializeField] TextMeshProUGUI fullWeightLabel;

    private void Awake()
    {
        //When a new ListItem is instantiated quantity is supposed to be 1. The reason it is in awake and not in start is that the initial adding of ListItems in ProductList is called alongside instantiate(), and since only awake is called alongside instantiate and has to wait for the next frame update, the quantity has to be set in awake (so that when ProductsList adds another product of same type, the quantity will properly start incrementing from 1, and not the int default value of 0)
        quantity = 1;
        SetQuantityLabelText(quantity);
    }
    private void Start()
    {
        
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
        //For whatever reason 
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
        Quantity = newQuantity;
    }
    public void UpdateUIValues()
    {
        listItemImageComponent.sprite = product.sprite;
        productNameLabel.text = product.name;

        SetPriceLabelText(product.price);
        SetProductWeightLabelText(product.weight);

        FullWeight = product.weight * Quantity;
        FullPrice = product.price * Quantity;
    }
    public void FixListItemProperties(int newQuantity)
    {
        Quantity = newQuantity;
        FullWeight = Quantity * product.weight;
        FullPrice = Quantity * product.price;
    }
    public void IncrementQuantity()
    {
        UpdateQuantity(++Quantity);
    }
    public void OnListItemClick()
    {
        modifyProductPanel.gameObject.SetActive(true);
        modifyProductPanel.InitializePanel(Quantity, FullWeight, this);
    }
    float GetFloatWIthTwoDecimalPlaces(float number)
    {
        return Mathf.Round(number*100) / 100;
    }
}
