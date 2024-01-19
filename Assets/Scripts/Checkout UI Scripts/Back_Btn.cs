using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Back_Btn : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject panelToClose;
    public void OnPointerClick(PointerEventData eventData)
    {
        panelToClose.SetActive(false);
    }
}
