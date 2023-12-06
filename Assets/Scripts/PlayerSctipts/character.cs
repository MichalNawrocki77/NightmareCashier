using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static UnityEngine.InputSystem.InputAction;

public class Character : MonoBehaviour
{
    public PlayerControls Input { get; private set; }

    #region movement

    Vector2 movementVector;

    Rigidbody2D rb;

    #endregion

    #region Checkout Interaction

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
        CurrentCheckout.Interact();
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
        rb.velocity = movementVector;
    }

    void ReadMovementInput(CallbackContext obj)
    {
        movementVector = obj.ReadValue<Vector2>();
    }
    //public void SetInteractionActionEnable(bool enable)
    //{
    //    switch (enable)
    //    {
    //        case true:
    //            Input.PlayerActionMap.InteractionAction.Enable();
    //            break;

    //        case false:
    //            Input.PlayerActionMap.InteractionAction.Disable();
    //            break;
    //    }
    //}
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
