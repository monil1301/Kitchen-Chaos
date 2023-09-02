using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class GameInput : MonoBehaviour
{
    // Public fields
    public static GameInput Instance;
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;
    public event EventHandler OnRebindKey;

    // Private fields
    private PlayerInputActions playerInputActions;

    // Unity methods
    private void Awake()
    {
        Instance = this;

        playerInputActions = new PlayerInputActions();

        // Check if player keys binding is saved
        if (PlayerPrefs.HasKey(Constants.PlayerPrefsKeys.PLAYER_KEY_BINDING))
        {
            // Load binding from player prefs using method from InputActions
            playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(Constants.PlayerPrefsKeys.PLAYER_KEY_BINDING));
        }

        playerInputActions.Enable();

        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
        playerInputActions.Player.Pause.performed += Pause_performed;
    }

    private void OnDestroy()
    {
        // Unsubcribe to the events to avoid issues on next game start
        playerInputActions.Player.Interact.performed -= Interact_performed;
        playerInputActions.Player.InteractAlternate.performed -= InteractAlternate_performed;
        playerInputActions.Player.Pause.performed -= Pause_performed;

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

    public string GetKeyBinding(KeyBinding binding)
    {
        // Used to get the key that is bound to each input action
        switch (binding)
        {
            default:
            // For Move actions index is used 1,2,3,4 as on 0 index name is save and from 1 actual keys are there
            case KeyBinding.MoveUp:
                return playerInputActions.Player.Move.bindings[1].ToDisplayString();
            case KeyBinding.MoveDown:
                return playerInputActions.Player.Move.bindings[2].ToDisplayString();
            case KeyBinding.MoveLeft:
                return playerInputActions.Player.Move.bindings[3].ToDisplayString();
            case KeyBinding.MoveRight:
                return playerInputActions.Player.Move.bindings[4].ToDisplayString();
            // For other inputs keyboard is first so index 0 for all keyboard inputs
            case KeyBinding.Interact:
                return playerInputActions.Player.Interact.bindings[0].ToDisplayString();
            case KeyBinding.InteractAlternate:
                return playerInputActions.Player.InteractAlternate.bindings[0].ToDisplayString();
            case KeyBinding.Pause:
                return playerInputActions.Player.Pause.bindings[0].ToDisplayString();
            // For other inputs gamepad is second so index 1 is used for all gamepad inputs
            case KeyBinding.Gamepad_Interact:
                return playerInputActions.Player.Interact.bindings[1].ToDisplayString();
            case KeyBinding.Gamepad_InteractAlternate:
                return playerInputActions.Player.InteractAlternate.bindings[1].ToDisplayString();
            case KeyBinding.Gamepad_Pause:
                return playerInputActions.Player.Pause.bindings[1].ToDisplayString();
        }
    }

    public void RebindKey(KeyBinding keyBinding, Action onRebindComplete)
    {
        // Disable the Player input while rebinding the key
        playerInputActions.Player.Disable();

        InputAction inputAction;
        int bindingIndex;

        // Get the input action and binding index for the action
        switch (keyBinding)
        {
            default:
            case KeyBinding.MoveUp:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 1;
                break;
            case KeyBinding.MoveDown:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 2;
                break;
            case KeyBinding.MoveLeft:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 3;
                break;
            case KeyBinding.MoveRight:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 4;
                break;
            case KeyBinding.Interact:
                inputAction = playerInputActions.Player.Interact;
                bindingIndex = 0;
                break;
            case KeyBinding.InteractAlternate:
                inputAction = playerInputActions.Player.InteractAlternate;
                bindingIndex = 0;
                break;
            case KeyBinding.Pause:
                inputAction = playerInputActions.Player.Pause;
                bindingIndex = 0;
                break;
            case KeyBinding.Gamepad_Interact:
                inputAction = playerInputActions.Player.Interact;
                bindingIndex = 1;
                break;
            case KeyBinding.Gamepad_InteractAlternate:
                inputAction = playerInputActions.Player.InteractAlternate;
                bindingIndex = 1;
                break;
            case KeyBinding.Gamepad_Pause:
                inputAction = playerInputActions.Player.Pause;
                bindingIndex = 1;
                break;
        }

        // Performs rebinding of key
        inputAction.PerformInteractiveRebinding(bindingIndex)
            .OnComplete(callback =>
            {
                // Disposing callback to avoid memory issues
                callback.Dispose();

                // Enable player input action once rebinding is completed
                playerInputActions.Player.Enable();
                onRebindComplete(); // Invoke callback 

                // Save the binding of the key
                PlayerPrefs.SetString(Constants.PlayerPrefsKeys.PLAYER_KEY_BINDING, playerInputActions.SaveBindingOverridesAsJson());
                PlayerPrefs.Save();

                OnRebindKey?.Invoke(this, EventArgs.Empty);
            })
            .Start(); // Starts the rebinding process
    }
}
