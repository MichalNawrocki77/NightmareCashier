using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OpenPanel_Btn : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject panelToOpen;
    public void OnPointerClick(PointerEventData eventData)
    {
        panelToOpen.SetActive(true);
    }
}
