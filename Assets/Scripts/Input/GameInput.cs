using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    // Private fields
    PlayerInputActions playerInputActions;

    // Unity methods
    private void Awake() 
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
    }

   // Public methods
   public Vector2 GetMovementVectorNormalised()
   {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        inputVector.Normalize();

        return inputVector;
   }
}
