using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.TerrainTools;

public class DayCycle : Singleton<DayCycle>
{
    
    [SerializeField] GameObject coworkerPad;

    

    public int howLongEventFail;
    [SerializeField] int howLongToAcceptEvent;

    public float howLongToCleanEvent;

    [SerializeField]
    AudioSource audioSource;

    

    public bool eventStarted;
    
    public GameObject currEvent;

    [SerializeField] GameObject clockTime;

    [SerializeField] GameObject eventDisclaimer;


    [SerializeField] GameObject endScreen;

    private void Awake()
    {
        ReadValuesFromSaveFile();

        //GenerateEventTimestamps();

    }

    
    /// <summary>
    /// This is just a placeholder, I put code that does not use a save file, but in the future I will probably use one.
    /// </summary>
    void ReadValuesFromSaveFile()
    {
        audioSource.volume = PlayerPrefs.GetFloat("MusicVolume");

        string[] gettedSaves = PlayerPrefs.GetString(PlayerPrefs.GetString("currentSave")).Split(";");

        //Jeœli jest to pierwszy dzieñ, ustaw jego d³ugoœæ na 200s, jesli to jakiœ inny dzieñ, ustaw jego czas na 300s
        switch (int.Parse(gettedSaves[0]))
        {
            case 1:
                //DayTimeLeft = 200;
                break;


            default:
                //DayTimeLeft = 300;
                break;
        }
    }
    

    private void GenerateMinMaxTimeBetweenEvents()
    {
        //int actualFullTime = FullDayTime - minTimeBeforeFirstEvent - minTimeAfterLastEvent;
        //maxTimeBetweenEvents = actualFullTime / howManyEvents;
        ////subtract rougly 25% from maxTime (I say roughly because dividing 2 ints will always give you an int, thus this operation has some error to it)
        ////The error is actually what I wasnt, I don't want floats in here
        //minTimeBetweenEvents = maxTimeBetweenEvents - (maxTimeBetweenEvents / 4);
        //Debug.Log("minTimeBetweenEvents: "+minTimeBetweenEvents);
        //Debug.Log("maxTimeBetweenEvents: " + maxTimeBetweenEvents);
    }

    private void EventNumberGenerator()
    {  
        //for (int i = 0; i < howManyEvents; i++)
        //{
        //    timestamps.Add((int)Random.Range(DayTimeLeft - minTimeBeforeFirstEvent, minTimeAfterLastEvent));
        //}

        //bool wrong = false;
        //for(int j = 1; j < timestamps.Count; j++)
        //{
        //    if(timestamps[j] - timestamps[j-1] < minTimeBetweenEvents || timestamps[j] - timestamps[j - 1] > maxTimeBetweenEvents)
        //    {
        //        wrong = true;
        //    }
        //}
        //if (wrong)
        //{
        //    timestamps.Clear();
        //    EventNumberGenerator();
        //}
    }


 
    

    // Update is called once per frame
    

    public  bool itsok = false;
    public bool itsokEvent = false;
    IEnumerator TooLate()
    {
     
        yield return new WaitForSecondsRealtime(howLongToAcceptEvent);

        if (itsok)
        {
            yield break;
        }

        DayManager.Instance.AddStrike();
        HideEventDisclaimer();
        //DayTimeLeft -= 1;
        coworkerPad.GetComponent<BoxCollider2D>().enabled = false;
        coworkerPad.GetComponent<SpriteRenderer>().color = Color.white;

    }

    public void ResetEventsVariables()
    {
        currEvent = null;
        itsok = false;
        itsokEvent = false;
        StopCoroutine(TooLate());
    }
    public void ShowEventDisclaimer()
    {
        eventDisclaimer.SetActive(true);
    }
    public void HideEventDisclaimer()
    {
        eventDisclaimer.SetActive(false);
    }
    public void ChangeShifts()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SHOP");
    }
    public void EndShift()
    {

        Time.timeScale = 1;
        SceneManager.LoadScene("MENU");
    }
}

