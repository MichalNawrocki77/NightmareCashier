using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class DayCycle : Singleton<DayCycle>
{

    [SerializeField]  public float DayTimeLeft;
    public bool StopCustomers;
    [SerializeField] GameObject coworkerPad;


    [SerializeField] int howMuchAfterStartEvents;
    [SerializeField] int howMuchBeforeEndEvents;
    [SerializeField] int maxBetweenEvents;
    [SerializeField] int minBetweenEvents;
    [SerializeField] int howManyEvents;

    public int howLongEventFail;
    [SerializeField] int howLongToAcceptEvent;

    public float howLongToCleanEvent;

    [SerializeField]

    AudioSource source;

    public List<int> timestamps = new List<int>();

    public bool eventStarted;
    
    public GameObject currEvent;

    [SerializeField] GameObject clockTime;

    [SerializeField] GameObject eventDisclaimer;


    [SerializeField] GameObject endScreen;



    void GenerateEventTimestamps()
    {
        bool canExit = false;

        while (!canExit)
        {
            timestamps.Clear();

            for (int i = 0; i < howManyEvents; i++)
            {
                timestamps.Add(Random.Range(howMuchAfterStartEvents, (int)DayTimeLeft - howMuchBeforeEndEvents));
            }
            timestamps.Sort();

            for(int i = 1;i< timestamps.Count; i++)
            {
                if ( (timestamps[i] - timestamps[i-1] > maxBetweenEvents)
                    ||
                    (timestamps[i] - timestamps[i - 1] < minBetweenEvents))
                {
                    break;
                }
                canExit = true;
            }
        }

        
    }
    private void EventNumberGenerator()
    {  
        for (int i = 0; i < howManyEvents; i++)
        {
            timestamps.Add((int)Random.Range(DayTimeLeft - howMuchAfterStartEvents, howMuchBeforeEndEvents));
        }

        bool wrong = false;
        for(int j = 1; j < timestamps.Count; j++)
        {
            if(timestamps[j] - timestamps[j-1] < minBetweenEvents || timestamps[j] - timestamps[j - 1] > maxBetweenEvents)
            {
                wrong = true;
            }
        }
        if (wrong)
        {
            timestamps.Clear();
            EventNumberGenerator();
        }
    }


 
    private void Awake()
    {
        source.loop = true;

        source.Play();


        string[] gettedSaves =  PlayerPrefs.GetString(PlayerPrefs.GetString("currentSave")).Split(";");
     
        //Jeœli jest to pierwszy dzieñ, ustaw jego d³ugoœæ na 200s, jesli to jakiœ inny dzieñ, ustaw jego czas na 300s
         switch (int.Parse(gettedSaves[0]))
         {
             case 1:
                 DayTimeLeft = 200;
                 break;


             default:
                DayTimeLeft = 300;
                 break;
         }

        StopCustomers = false;

        

        if (howMuchAfterStartEvents + howMuchBeforeEndEvents + maxBetweenEvents * howManyEvents > DayTimeLeft)
        {
            Debug.LogError("NIE DA SIE WYGENEROWAC TYLU PROBLEMOW W TAK KROTKIM CZASIE");
        }
        else
        {
            GenerateEventTimestamps();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        source.volume = PlayerPrefs.GetFloat("MusicVolume");






        if (currEvent == null)
        { 
            eventStarted = false;
        }
        else
        {
            eventStarted = true;
        }

        if (DayTimeLeft >= 0 && eventStarted == false)
        {

            DayTimeLeft -= Time.fixedDeltaTime;

            clockTime.GetComponent<TMPro.TextMeshProUGUI>().text = $"SHIFT TIME: {Mathf.Floor(DayTimeLeft)} min";
        }
       
        if(DayTimeLeft <= 0)
        {
            StopCustomers = true;

            //string[] gettedSaves = PlayerPrefs.GetString(PlayerPrefs.GetString("currentSave")).Split(";"); 

          //  PlayerPrefs.SetString(PlayerPrefs.GetString("currentSave"), (int.Parse(gettedSaves[0]) +1).ToString() + ";0");

            Time.timeScale = 0;
            endScreen.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = $"Gratulacje przetrwa³eœ ca³¹ noc, iloœæ twoich b³êdów to: { DayManager.Instance.strikes}";
            endScreen.SetActive(true);
        }



        for(int i = 0; i < timestamps.Count; i++)
        {
            if (Mathf.Round(DayTimeLeft) == timestamps[i])
            {
                StartCoroutine(TooLate());
                ShowEventDisclaimer();
                DayTimeLeft -= 1;
                coworkerPad.GetComponent<BoxCollider2D>().enabled = true;
                coworkerPad.GetComponent<SpriteRenderer>().color = Color.green;
                break;
            }
        }
    }

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
        DayTimeLeft -= 1;
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
        SceneManager.LoadScene("SHOP");
    }
    public void EndShift()
    {
        SceneManager.LoadScene("MENU");
    }


}
