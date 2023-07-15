using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
   // Serialized fields
   [SerializeField] private float moveSpeed = 7;
   [SerializeField] private GameInput gameInput;

   // Private fields
   private bool isWalking = false;
   
   // Unity Methods
   private void Update() 
   {
      // Get input from GameInput script
      Vector2 inputVector = gameInput.GetMovementVectorNormalised();

      // Calculate distance and direction to be moved
      float moveDistance = moveSpeed * Time.deltaTime;
      Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

      // Check if player can move the distance in that direction i.e check for collisons
      bool canPlayerMove = CanPlayerMove(moveDirection, moveDistance); 

      // If player cannot move, check for one direction X or Z 
      if (!canPlayerMove)
      {
         // Check for X movement
         Vector3 moveDirectionX = new Vector3(moveDirection.x, 0, 0).normalized;
         canPlayerMove = CanPlayerMove(moveDirectionX, moveDistance);
         if (canPlayerMove)
         {
            moveDirection = moveDirectionX;
         }
         else
         {
            //Check for Z movement
            Vector3 moveDirectionZ = new Vector3(0, 0, moveDirection.y).normalized;
            canPlayerMove = CanPlayerMove(moveDirectionZ, moveDistance);
            if (canPlayerMove)
            {
               moveDirection = moveDirectionZ;
            }
         }
      }

      // Apply the input to the gameobject
      if (canPlayerMove)
      {
         transform.position += (moveDirection * moveDistance);
      }

      isWalking = moveDirection != Vector3.zero;

      // Apply rotation to the gameobject to be facing the direction of moving
      float rotateSpeed = 10f;
      transform.forward = Vector3.Slerp(transform.forward, moveDirection, rotateSpeed * Time.deltaTime); // Slerp to smoothly move between vectors
   }

   // Private Methods
   private bool CanPlayerMove(Vector3 moveDirection, float moveDistance)
   {
      float playerHeight = 2f;
      float playerRadius = 0.7f;

      bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirection, moveDistance);
      return canMove;
   }

   // Public Methods
   public bool IsWalking()
   {
      return isWalking;
   }
}
