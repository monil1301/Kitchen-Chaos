using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class GameInput : MonoBehaviour
{
    // Public fields
    public event EventHandler OnInteractAction;

    // Private fields
    private PlayerInputActions playerInputActions;

    // Unity methods
    private void Awake() 
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();

        playerInputActions.Player.Interact.performed +=  Interact_performed;
    }

    // Private methods
    private void Interact_performed(CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    // Public methods
    public Vector2 GetMovementVectorNormalised()
    {
            Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
            inputVector.Normalize();

            return inputVector;
    }
}
