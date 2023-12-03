using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static UnityEngine.InputSystem.InputAction;

public class character : MonoBehaviour
{
    PlayerControls input;

    #region movement

    Vector2 movementVector;

    Rigidbody2D rb;

    #endregion

    private void Awake()
    {
        input = new PlayerControls();

        input.PlayerInGameWorld.Enable();
        input.PlayerInGameWorld.MovementAction.performed += ReadMovementInput;

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
}
