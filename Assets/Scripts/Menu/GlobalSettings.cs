using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalSettings : MonoBehaviour
{
    [SerializeField]
    GameObject slider1;
    [SerializeField]
    GameObject slider2;

  

    private void Awake()
    {

        slider1.GetComponent<Slider>().value = PlayerPrefs.GetFloat("MusicVolume");
        slider2.GetComponent<Slider>().value = PlayerPrefs.GetFloat("EffectVolume");
    }

    public void plusVolumeMusic(GameObject slider)
    {
   
        slider.GetComponent<Slider>().value += 0.1f;
        PlayerPrefs.SetFloat("MusicVolume", slider.GetComponent<Slider>().value);

    }

    public void minusVolumeMusic(GameObject slider)
    {

        slider.GetComponent<Slider>().value -= 0.1f;
        PlayerPrefs.SetFloat("MusicVolume", slider.GetComponent<Slider>().value);

    }


    public void plusVolumeEffect(GameObject slider)
    {

        slider.GetComponent<Slider>().value += 0.1f;
        PlayerPrefs.SetFloat("EffectVolume", slider.GetComponent<Slider>().value);

    }

    public void minusVolumeEffect(GameObject slider)
    {

        slider.GetComponent<Slider>().value -= 0.1f;
        PlayerPrefs.SetFloat("EffectVolume", slider.GetComponent<Slider>().value);

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
