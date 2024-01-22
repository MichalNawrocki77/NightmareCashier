using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventClean : MonoBehaviour
{
 
    bool inCol = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (DayCycle.Instance.currEvent != null)
            {
                inCol = true;
            }
        }
    }


    private void Update()
    {
        if (inCol)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(Wait());
            }
        }
    }


    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
        DayCycle.Instance.itsokEvent = true;
        DayCycle.Instance.HideEventShowImage();
        inCol = false;
        DayCycle.Instance.currEvent = null;
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (DayCycle.Instance.currEvent != null)
            {
                inCol = false;
            }
        }
    }
}
