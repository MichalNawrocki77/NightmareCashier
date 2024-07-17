using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.InputSystem;

public class ShopEvent : MonoBehaviour
{
    [SerializeField] Player player;
    SpriteRenderer spriteRenderer;
    bool inCol = false;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
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
        player.DisableMovement();

        Color fadeAwayColor = spriteRenderer.color;
        float timeElapsed = 0f;
        while (timeElapsed < DayCycle.Instance.howLongToCleanEvent)
        {
            fadeAwayColor.a = Mathf.Lerp(1, 0, timeElapsed / DayCycle.Instance.howLongToCleanEvent);
            spriteRenderer.color = fadeAwayColor;
            timeElapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        player.EnableMovement();
        DayCycle.Instance.itsokEvent = true;
        DayCycle.Instance.HideEventDisclaimer();
        inCol = false;
        DayCycle.Instance.currEvent = null;
        spriteRenderer.color = new Color(1, 1, 1, 1);
        gameObject.SetActive(false);
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
