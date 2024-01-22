using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoworkerPadInteract : MonoBehaviour
{


    //[SerializeField] GameObject EventToActivate;

    int ran = 0;
    [SerializeField] List<GameObject> possibleEvents;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            DayCycle.Instance.itsok = true;

           GetComponent<BoxCollider2D>().enabled = false;
           GetComponent<SpriteRenderer>().color = Color.white;

           ran = Random.Range(0, possibleEvents.Count);
           possibleEvents[ran].SetActive(true);
           DayCycle.Instance.currEvent = possibleEvents[Random.Range(0,possibleEvents.Count)];

            StartCoroutine(Fail());


        }


    }


    IEnumerator Fail()
    {
        yield return new  WaitForSecondsRealtime(DayCycle.Instance.howLongEventFail);

        if (DayCycle.Instance.itsokEvent)
        {
            DayCycle.Instance.ResetEventsVariables();
            yield break;
        }
        possibleEvents[ran].SetActive(false);
        DayManager.Instance.AddStrike();
        DayCycle.Instance.HideEventShowImage();
        DayCycle.Instance.ResetEventsVariables();

    }
}
