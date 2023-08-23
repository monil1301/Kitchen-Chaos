using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class GameInput : MonoBehaviour
{
    // Public fields
    public static GameInput Instance;
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;

    // Private fields
    private PlayerInputActions playerInputActions;

    // Unity methods
    private void Awake() 
    {
        Instance = this;

        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();

        playerInputActions.Player.Interact.performed +=  Interact_performed;
        playerInputActions.Player.InteractAlternate.performed +=  InteractAlternate_performed;
        playerInputActions.Player.Pause.performed +=  Pause_performed;
    }

    private void OnDestroy() 
    {
        // Unsubcribe to the events to avoid issues on next game start
        playerInputActions.Player.Interact.performed -=  Interact_performed;
        playerInputActions.Player.InteractAlternate.performed -=  InteractAlternate_performed;
        playerInputActions.Player.Pause.performed -=  Pause_performed;

        // Destroy the player input action
        playerInputActions.Dispose();
    }

    // Private methods
    private void Interact_performed(CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_performed(CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }
    
    private void Pause_performed(CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    // Public methods
    public Vector2 GetMovementVectorNormalised()
    {
            Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
            inputVector.Normalize();

            return inputVector;
    }
}
