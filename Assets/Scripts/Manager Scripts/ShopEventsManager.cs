using System.Collections;
using System.Collections.Generic;
using UnityEditor;

using UnityEngine;

public class ShopEventsManager : MonoBehaviour
{


    [Tooltip("If you wish to specify both minTimeBetweenShopEvents and maxTimeBetweenShopEvents you need to set this to true, otherwise the script will try to calculate it's own values to distribute shop events more evenly")]
    [HideInInspector] public bool useManualTimeBetweenShopEvents;
    [HideInInspector] public int maxTimeBetweenShopEvents;
    [HideInInspector] public int minTimeBetweenShopEvents;

    [SerializeField] int TimeBeforeFirstShopEvent;
    [SerializeField] int TimeAfterLastShopEvent;
    [SerializeField] int timeToTakeCareOfShopEvent;

    [SerializeField] int howManyShopEvents;

    [SerializeField] GameObject ShopEventDisclaimerPanel;

    [SerializeField]
    int[] timestamps;
    int currentEventIndex;

    Coroutine currentCoroutine;

    private void Awake()
    {
        GenerateEventTimestamps();
        currentEventIndex = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        DayManager.Instance.OnSecondPassed += CheckForShopEvents;
    }

    // Update is called once per frame
    void Update()
    {

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

        timestamps[0] = (Random.Range(TimeBeforeFirstShopEvent,
                TimeBeforeFirstShopEvent + maxTimeBetweenShopEvents));

        for (int i = 1; i < howManyShopEvents; i++)
        {
            timestamps[i] = (Random.Range(
                timestamps[i - 1] + minTimeBetweenShopEvents,
                timestamps[i - 1] + maxTimeBetweenShopEvents)
                );
        }

    }

    private void GenerateMinMaxTimeBetweenShopEvents()
    {
        int actualFullTime = DayManager.Instance.FullDayTime - TimeBeforeFirstShopEvent - TimeAfterLastShopEvent;

        if (actualFullTime / howManyShopEvents < timeToTakeCareOfShopEvent)
        {
            Debug.Log("Too many events, time between them is lower than time to take care of it. No events were generated");
            return;
        }

        maxTimeBetweenShopEvents = actualFullTime / howManyShopEvents;
        //subtract rougly 25% from maxTime (I say roughly because dividing 2 ints will always give you an int, thus this operation has some error to it)
        //The error is actually what I wasnt, I don't want floats in here
        minTimeBetweenShopEvents = maxTimeBetweenShopEvents - (maxTimeBetweenShopEvents / 4);
        Debug.Log("minTimeBetweenEvents: " + minTimeBetweenShopEvents);
        Debug.Log("maxTimeBetweenEvents: " + maxTimeBetweenShopEvents);
    }

    void CheckForShopEvents()
    {
        if(DayManager.Instance.DayTimeLeft == timestamps[currentEventIndex])
        {
            StartShopEvent();
            currentEventIndex++;
        }
    }
    void StartShopEvent()
    {
        ShowShopEventUI();

        currentCoroutine = StartCoroutine(ShopEventCountdown());

    }
    
    IEnumerator ShopEventCountdown()
    {

        int timeLeft = timeToTakeCareOfShopEvent;
        while(timeLeft > 0)
        {
            yield return new WaitForSecondsRealtime(1);
            timeLeft--;
        }

        ShopEventFailed();
    }

    private void ShopEventFailed()
    {
        Debug.Log("Shop Event Failed");
    }

    private void ShowShopEventUI()
    {
        ShopEventDisclaimerPanel.SetActive(true);
    }

    private void OnDestroy()
    {
        //I never intend on Destroying EventsManager, but in case in the future that changes I put events unsubscribing to prevent memory leaks. Hope future me will appreciate
        DayManager.Instance.OnSecondPassed -= CheckForShopEvents;
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
