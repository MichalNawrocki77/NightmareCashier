using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
public class ConfirmPurchase_Btn : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject acceptPurchasePanel;
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"Kliknieto przycisk {this.gameObject.name}");
        acceptPurchasePanel.SetActive(true);
    }
}
