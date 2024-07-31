using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] GameObject eventDisclaimerPanel;
    [SerializeField] EndScreenPanel endScreenPanel;
    [SerializeField] GameObject inGameUIPanel;

    [SerializeField] TextMeshProUGUI strikeText;
    [SerializeField] TextMeshProUGUI clockText;

    public void EndDay()
    {
        inGameUIPanel.SetActive(false);
        endScreenPanel.GameEnd();

    }

    public void UpdateStrikeUI(int strikes)
    {
        //if by any chance you want to do anything else while updating the UI, Do it here.
        strikeText.text = $"Strike: {strikes}";
    }

    public void UpdateClockUI(int dayTimeLeft)
    {
        clockText.text = $"SHIFT TIME: {dayTimeLeft} min";
    }
    public void ShowShopEventUI()
    {
        eventDisclaimerPanel.SetActive(true);
    }

    public void HideShipEventUI()
    {
        eventDisclaimerPanel.SetActive(false);
    }
}
