using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class ModifyProduct_Panel : MonoBehaviour
{
    [HideInInspector]
    public Products_ListItem currentListItem;

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
            FullWeight = Quantity * currentListItem.product.weight;
        }
    }
    int quantity;

    public float FullWeight
    {
        get
        {
            return fullWeight;
        }
        set
        {
            fullWeight = value;
            SetFullWeightLabelText(fullWeight);
        }
    }
    private float fullWeight;

    [SerializeField] Image productImage;
    [SerializeField] TextMeshProUGUI productName_lbl;
    [SerializeField] TextMeshProUGUI productQuantity_lbl;
    [SerializeField] TextMeshProUGUI fullWeight_lbl;

    #region labels text setters
    void SetQuantityLabelText(int quantity)
    {
        productQuantity_lbl.text = $"{quantity} pcs";
    }
    void SetFullWeightLabelText(float weight)
    {
        fullWeight_lbl.text = $"{weight}kg";
    }
    #endregion

    public void InitializePanel(int initialQuantity, float initialFullWeight, Products_ListItem listItem)
    {
        currentListItem = listItem;

        productImage.sprite = currentListItem.product.sprite;
        productName_lbl.text = currentListItem.product.name;
        Quantity = initialQuantity;
        FullWeight = initialFullWeight;
    }
    public void PlusButtonClicked()
    {
        Quantity++;
    }
    public void MinusButtonClicked()
    {
        Quantity--;
    }
    public void ModifyProductButtonClicked()
    {
        currentListItem.FixListItemProperties(Quantity);
        gameObject.SetActive(false);
    }
    public void WeighAgainButtonClicked()
    {
        FullWeight = Quantity * currentListItem.product.weight;
    }
}
