using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AcceptPurchase_Panel : MonoBehaviour
{
    [SerializeField] Interaction interaction;
    [SerializeField] ProductsList productsList;
    public void OnAcceptPurchaseClicked()
    {
        interaction.isAcceptClicked = true;
    }
}
