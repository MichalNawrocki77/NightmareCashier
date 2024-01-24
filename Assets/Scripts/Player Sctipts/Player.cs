using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static UnityEngine.InputSystem.InputAction;

public class Player : MonoBehaviour
{
    public PlayerControls Input { get; private set; }

    #region movement

    Vector2 movementVector;

    Rigidbody2D rb;

    #endregion

    #region Checkout Interaction

    public Action InteractionPressed;
    public Checkout CurrentCheckout { private get; set; }

    #endregion

    private void Awake()
    {
        Input = new PlayerControls();

        Input.PlayerActionMap.Enable();
        Input.PlayerActionMap.MovementAction.performed += ReadMovementInput;
        Input.PlayerActionMap.InteractionAction.performed += InteractionAction_performed;

        //Disabling at the start of the game, so that I can't "interact" with nothing from the get go
        Input.PlayerActionMap.InteractionAction.Disable();
    }

    private void InteractionAction_performed(CallbackContext obj)
    {
        //I know I can do the same by calling InteractionPressed?.Invoke() but I really want to have an error in the console when it reads a null, will help with future debugging
        if (InteractionPressed != null)
        {
            InteractionPressed.Invoke();
            return;
        }
        Debug.Log("InteractionPressed jest NULL!!!");

    }
    public void DisableMovement()
    {
        Input.PlayerActionMap.MovementAction.Disable();
        movementVector = Vector2.zero;
    }
    public void EnableMovement()
    {
        Input.PlayerActionMap.MovementAction.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        rb.velocity = movementVector * 4f;
    }

    void ReadMovementInput(CallbackContext obj)
    {
        movementVector = obj.ReadValue<Vector2>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Checkout"))
        {            
            CurrentCheckout = collision.GetComponent<Checkout>();
            Input.PlayerActionMap.InteractionAction.Enable();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Checkout"))
        {
            Input.PlayerActionMap.InteractionAction.Disable();
            CurrentCheckout = null;
        }        
    }
}
