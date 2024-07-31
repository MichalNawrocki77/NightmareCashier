using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class EndScreenPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI endGameText;

    public void GameEnd()
    {
        this.gameObject.SetActive(true);
        endGameText.text = $"Gratulacje przetrwa�e� ca�� noc, ilo�� twoich b��d�w to: {DayManager.Instance.strikes}";
    }
}
