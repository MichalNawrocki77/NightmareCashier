using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    [SerializeField]
    AudioSource source;
    public void ExitApp()
    {
        //Application.Quit();
        Debug.Log("quit");



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
