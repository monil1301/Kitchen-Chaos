using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
   // Serialized fields
   [SerializeField] private float moveSpeed = 7;
   
   // Unity Methods
   private void Update() 
   {
      Vector2 inputVector = new Vector2(0, 0);

      // Move Left
      if (Input.GetKey(KeyCode.W))
      {
         inputVector.y = 1;
      }

      // Move Right
      if (Input.GetKey(KeyCode.S))
      {
         inputVector.y = -1;
      }

      // Move Up
      if (Input.GetKey(KeyCode.A))
      {
         inputVector.x = -1;
      }

      // Move Down
      if (Input.GetKey(KeyCode.D))
      {
         inputVector.x = 1;
      }

      inputVector.Normalize();
      Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);
      transform.position += (moveDirection * moveSpeed * Time.deltaTime);
   }

   // Private Methods

   // Public Methods
}
