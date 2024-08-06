using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.InputSystem;

public class ShopEvent : MonoBehaviour
{
    Player player;
    SpriteRenderer spriteRenderer;
    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        if (player is null) Debug.LogError($"{gameObject.name} did not find Player");

        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.AssignInteractionAction(InteractWithEvent);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.UnassignIntarctionAction();
        }
    }

    void InteractWithEvent()
    {
        StartCoroutine(InteractionWithEventCoroutine());
    }

    IEnumerator InteractionWithEventCoroutine()
    {
        player.DisableMovement();

        //Code that fades away the interaction object
        Color fadeAwayColor = spriteRenderer.color;
        float timeElapsed = 0f;
        while (timeElapsed < DayCycle.Instance.howLongToCleanEvent)
        {
            fadeAwayColor.a = Mathf.Lerp(1, 0, timeElapsed / DayCycle.Instance.howLongToCleanEvent);
            spriteRenderer.color = fadeAwayColor;
            //I use deltaTime and not fixedDeltaTime since I change the way it looks, so it makes sense to change by the delta of rendered frame
            timeElapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        player.UnassignIntarctionAction();
        player.EnableMovement();

        ShopEventsManager.Instance.EndShopEvent();

        spriteRenderer.color = new Color(1, 1, 1, 1);
        gameObject.SetActive(false);
    }
}
