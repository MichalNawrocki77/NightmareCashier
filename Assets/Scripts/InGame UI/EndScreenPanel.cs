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
        endGameText.text = $"Gratulacje przetrwa³eœ ca³¹ noc, iloœæ twoich b³êdów to: {DayManager.Instance.strikes}";
    }
}
