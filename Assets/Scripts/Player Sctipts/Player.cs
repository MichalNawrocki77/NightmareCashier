using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static UnityEngine.InputSystem.InputAction;

public class Player : MonoBehaviour
{

    public PlayerControls Input { get; private set; }

    public delegate void InteractionButtonCallback();
    InteractionButtonCallback onInteractionButtonPressed;

    #region movement

    Vector2 movementVector;

    Rigidbody2D rb;

    #endregion

    #region Checkout Interaction

    public Checkout CurrentCheckout { private get; set; }

    #endregion

    #region Shelves

    //technically I could use a stateMachine for keeping track of which box I have, but that seems like an overkill
    [HideInInspector] public ProductSO currentlyHeldBox;

    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite defaultSprite;

    [Tooltip("index of this array = (int)productSO.type. Make sure the order is correct")]
    [SerializeField] Sprite[] spritesWithBoxes;

    #endregion

    private void Awake()
    {
        Input = new PlayerControls();

        Input.PlayerActionMap.Enable();
        Input.PlayerActionMap.MovementAction.performed += ReadMovementInput;
        Input.PlayerActionMap.InteractionAction.performed += InteractionAction_performed;

        //Disabling at the start of the game, so that I can't "interact" with nothing from the get go
        Input.PlayerActionMap.InteractionAction.Disable();

        spriteRenderer = GetComponent<SpriteRenderer>();
        currentlyHeldBox = null;
    }

    private void InteractionAction_performed(CallbackContext obj)
    {
        //I know I can do the same by calling InteractionPressed?.Invoke() but I really want to have an error in the console when it reads a null, it might help with future debugging
        if (onInteractionButtonPressed != null)
        {
            onInteractionButtonPressed();
            return;
        }
        Debug.Log("InteractionPressed is NULL!!!");

    }
    

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
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
    public void DisableMovement()
    {
        Input.PlayerActionMap.MovementAction.Disable();
        movementVector = Vector2.zero;
    }
    public void EnableMovement()
    {
        Input.PlayerActionMap.MovementAction.Enable();
    }

    public void AssignInteractionAction(InteractionButtonCallback callback)
    {
        Input.PlayerActionMap.InteractionAction.Enable();
        onInteractionButtonPressed = callback;
    }
    public void UnassignIntarctionAction()
    {
        onInteractionButtonPressed = null;
        Input.PlayerActionMap.InteractionAction.Disable();
    }

    public void PickUpBox(ProductSO pickedUpProductBox)
    {
        currentlyHeldBox = pickedUpProductBox;
        spriteRenderer.sprite = spritesWithBoxes[(int)pickedUpProductBox.type];
    }
    public void PutDownbox()
    {
        currentlyHeldBox = null;
        spriteRenderer.sprite = defaultSprite;
    }
}
