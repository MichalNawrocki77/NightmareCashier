using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AcceptPurchaseConfirmation_Btn : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject interaction;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"Kliknieto przycisk {this.gameObject.name}");
        /*
         
        Code that checks wheter or not the user won the interaction and informs something (probably the day manager) about the result
         
         */
        Destroy(interaction);
    }
}
