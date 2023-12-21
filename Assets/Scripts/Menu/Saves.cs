using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Saves : MonoBehaviour
{

    private void Awake()
    {
        if (PlayerPrefs.GetString("SAVE1") != "")
        {
            save1.transform.GetChild(0).GetComponent<TMP_Text>().text = "shift: " + PlayerPrefs.GetString("SAVE1")[0];

        }
        if (PlayerPrefs.GetString("SAVE2") != "")
        {
             save2.transform.GetChild(0).GetComponent<TMP_Text>().text = "shift: " + PlayerPrefs.GetString("SAVE2")[0];
        }

        if (PlayerPrefs.GetString("SAVE3") != "")
        {
            save3.transform.GetChild(0).GetComponent<TMP_Text>().text = "shift: " + PlayerPrefs.GetString("SAVE3")[0];
        }
    }


    public string whichSave;
    public GameObject save1;
    public GameObject save2;
    public GameObject save3;



    public void saveChoice(GameObject save)
    {
        save1.GetComponent<Image>().color = Color.white;
        save2.GetComponent<Image>().color = Color.white;
        save3.GetComponent<Image>().color = Color.white;

        whichSave = save.name;
        save.GetComponent<Image>().color = Color.gray;
    }

    public void deleteSave()
    {
        if (whichSave != "")
        {
            PlayerPrefs.DeleteKey(whichSave);
            GameObject.Find(whichSave).transform.GetChild(0).GetComponent<TMP_Text>().text = "SAVE (PUSTY)";
        }
    }

    public void loadSave()
    {
        if (whichSave != "")
        {
            if (PlayerPrefs.GetString(whichSave) ==  "" || PlayerPrefs.GetString(whichSave)[0] == '1')
            {
                Debug.Log("tworzymy gówno!!!");

                PlayerPrefs.SetString(whichSave,"2;5");

              

            }
            else
            {
                string[] omg = PlayerPrefs.GetString(whichSave).Split(";");
                Debug.Log("shift: " + omg[0] + " zagadek rozwi¹zanych: " + omg[1]);
            }


            GameObject.Find(whichSave).transform.GetChild(0).GetComponent<TMP_Text>().text = "shift: " + PlayerPrefs.GetString(whichSave)[0];
        
        }


       
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
