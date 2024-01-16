using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoworkerPadInteract : MonoBehaviour
{

    //[SerializeField] GameObject EventToActivate;

    [SerializeField] List<GameObject> possibleEvents;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

               GetComponent<BoxCollider2D>().enabled = false;
               GetComponent<SpriteRenderer>().color = Color.white;

            
           // possibleEvents[Random.Range(0, possibleEvents.Count + 1)].SetActive(true);
           // DayCycle.Instance.currEvent = possibleEvents[Random.Range(0,possibleEvents.Count)];


        }

    }
}
