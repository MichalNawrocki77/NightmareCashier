using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    [SerializeField]
    AudioSource source;
    public void ExitApp()
    {
        Debug.Log("quit");
        Application.Quit();
    }

    private void Update()
    {
        source.volume = PlayerPrefs.GetFloat("MusicVolume");
    }

    private void Start()
    {

        source.loop = true;

        source.Play();
    
        
    }


}
