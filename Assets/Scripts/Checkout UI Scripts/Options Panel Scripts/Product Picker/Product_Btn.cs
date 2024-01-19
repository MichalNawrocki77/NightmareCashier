using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Product_Btn : MonoBehaviour, IPointerClickHandler
{
    public Product product;
    [SerializeField] GameObject panel;
    public void OnPointerClick(PointerEventData eventData)
    {
        panel.SetActive(true);
        panel.GetComponent<AddProduct_Panel>().InitializePanel(product);
    }
}
