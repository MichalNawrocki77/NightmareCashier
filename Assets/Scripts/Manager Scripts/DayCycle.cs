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

