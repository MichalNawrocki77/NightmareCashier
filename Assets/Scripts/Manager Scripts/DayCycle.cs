using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public List<int> timestamps = new List<int>();

    public bool eventStarted;
    
    public GameObject currEvent;



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
         string[] gettedSaves =  PlayerPrefs.GetString(PlayerPrefs.GetString("currentSave")).Split(";");
     
        //Jeœli jest to pierwszy dzieñ, ustaw jego d³ugoœæ na 300s, jesli to jakiœ inny dzieñ, ustaw jego czas na 600s
         switch (int.Parse(gettedSaves[0]))
         {
             case 1:
                 DayTimeLeft = 300;
                 break;


             default:
                DayTimeLeft = 600;
                 break;
         }

        StopCustomers = false;

        if (howMuchAfterStartEvents + howMuchBeforeEndEvents + maxBetweenEvents * howManyEvents > DayTimeLeft)
        {
            Debug.LogError("NIE DA SIE WYGENEROWAC TYLU PROBLEMOW W TAK KROTKIM CZASIE");
        }
        else
        {
            EventNumberGenerator();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(currEvent == null)
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
        }
       
        if(DayTimeLeft <= 0)
        {
            StopCustomers = true;

            string[] gettedSaves = PlayerPrefs.GetString(PlayerPrefs.GetString("currentSave")).Split(";"); 

            PlayerPrefs.SetString(PlayerPrefs.GetString("currentSave"), (int.Parse(gettedSaves[0]) +1).ToString() + ";0");
            SceneManager.LoadScene("SHOP");

        }

        for(int i = 0; i < timestamps.Count; i++)
        {
            if (Mathf.Round(DayTimeLeft) == timestamps[i])
            {
                DayTimeLeft -= 1;
                coworkerPad.GetComponent<BoxCollider2D>().enabled = true;
                coworkerPad.GetComponent<SpriteRenderer>().color = Color.green;
                break;
            }
        }
    }
}
