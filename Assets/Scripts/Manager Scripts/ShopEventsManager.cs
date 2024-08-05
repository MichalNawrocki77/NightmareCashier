using System.Collections;
using System.Collections.Generic;
using UnityEditor;

using UnityEngine;

public class ShopEventsManager : Singleton<ShopEventsManager>
{


    [Tooltip("If you wish to specify both minTimeBetweenShopEvents and maxTimeBetweenShopEvents you need to set this to true, otherwise the script will try to calculate it's own values to distribute shop events more evenly")]
    [HideInInspector] public bool useManualTimeBetweenShopEvents;
    [HideInInspector] public int maxTimeBetweenShopEvents;
    [HideInInspector] public int minTimeBetweenShopEvents;

    [SerializeField] int TimeBeforeFirstShopEvent;
    [SerializeField] int TimeAfterLastShopEvent;
    [SerializeField] int timeToTakeCareOfShopEvent;

    [SerializeField] int howManyShopEvents;


    [Tooltip("Timestamps are serialized, only for the purpose of looking up generated timestamps at editor runtime")]
    [SerializeField]
    int[] timestamps;
    int currentEventIndex;

    [SerializeField] GameObject[] shopEvents;

    Coroutine currentCoroutine;

    private void Awake()
    {
        GenerateEventTimestamps();
    }
    private void OnEnable()
    {
        DayManager.Instance.OnSecondPassed += CheckForShopEvents;
    }
    private void OnDisable()
    {
        DayManager.Instance.OnSecondPassed -= CheckForShopEvents;
    }

    void GenerateEventTimestamps()
    {
        if (!useManualTimeBetweenShopEvents)
        {
            GenerateMinMaxTimeBetweenShopEvents();
        }

        if (TimeBeforeFirstShopEvent + TimeAfterLastShopEvent + maxTimeBetweenShopEvents * howManyShopEvents > DayManager.Instance.FullDayTime)
        {
            Debug.LogError("There can't be that many shop events in such a short time. No events were created");
            return;
        }

        timestamps = new int[howManyShopEvents];
        currentEventIndex = howManyShopEvents - 1;

        timestamps[0] = (Random.Range(TimeBeforeFirstShopEvent,
                TimeBeforeFirstShopEvent + maxTimeBetweenShopEvents));

        for (int i = 1; i < howManyShopEvents; i++)
        {
            timestamps[i] = (Random.Range(
                TimeBeforeFirstShopEvent + i * maxTimeBetweenShopEvents + minTimeBetweenShopEvents,
                TimeBeforeFirstShopEvent + i * maxTimeBetweenShopEvents + maxTimeBetweenShopEvents)
                );
        }
    }

    private void GenerateMinMaxTimeBetweenShopEvents()
    {
        int actualFullTime = DayManager.Instance.FullDayTime - TimeBeforeFirstShopEvent - TimeAfterLastShopEvent;

        if (actualFullTime / howManyShopEvents < timeToTakeCareOfShopEvent)
        {
            Debug.Log("Too many events, time between them is lower than time to take care of it.");
            return;
        }

        maxTimeBetweenShopEvents = actualFullTime / howManyShopEvents;
        //subtract rougly 25% from maxTime (I say roughly because dividing 2 ints will always give you an int, thus this operation has some error to it)
        //The error is actually what I want, I don't want floats in here
        minTimeBetweenShopEvents = maxTimeBetweenShopEvents - (maxTimeBetweenShopEvents / 4);
        
    }

    void CheckForShopEvents()
    {
        if(DayManager.Instance.DayTimeLeft == timestamps[currentEventIndex])
        {
            StartShopEvent();
        }
    }
    private void ChooseShopEventToTrigger()
    {
        shopEvents[Random.Range(0, shopEvents.Length)].SetActive(true);
    }
    void StartShopEvent()
    {
        DayManager.Instance.isDayTimeRunning = false;

        UIManager.Instance.ShowShopEventUI();

        ChooseShopEventToTrigger();

        currentCoroutine = StartCoroutine(ShopEventCountdown());

    }
    private void ShopEventFailed()
    {
        DayManager.Instance.AddStrike();

        //If Player didn't clear the ShopEvent, reset the countdown (it can't just dissapear out of nowhere)
        StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(ShopEventCountdown());
    }
    public void EndShopEvent()
    {
        StopCoroutine(currentCoroutine);
        DayManager.Instance.isDayTimeRunning = true;
        currentEventIndex--;
        UIManager.Instance.HideShipEventUI();
    }
    
    IEnumerator ShopEventCountdown()
    {

        int timeLeft = timeToTakeCareOfShopEvent + 1;
        while(timeLeft > 0)
        {
            timeLeft--;
            yield return new WaitForSecondsRealtime(1);
        }

        ShopEventFailed();
    }
}











[CustomEditor(typeof(ShopEventsManager))]
public class DayCycle_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        var script = target as ShopEventsManager;

        script.useManualTimeBetweenShopEvents = EditorGUILayout.Toggle("Set Time Between Events Manually", script.useManualTimeBetweenShopEvents);
        if (!script.useManualTimeBetweenShopEvents)
        {
            //if you don't want to set it manually then return
            base.OnInspectorGUI();
            return;
        }

        script.minTimeBetweenShopEvents = EditorGUILayout.IntField(
            "Minimum Time Between Events",
            script.minTimeBetweenShopEvents);
        script.maxTimeBetweenShopEvents = EditorGUILayout.IntField(
            "Maximum Time Between Events",
            script.maxTimeBetweenShopEvents);

        base.OnInspectorGUI();
    }
}
