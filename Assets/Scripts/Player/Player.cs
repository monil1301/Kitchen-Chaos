using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
   // Properties
   public static Player Instance { get; private set; }

   // Serialized fields
   [SerializeField] private float moveSpeed = 7;
   [SerializeField] private GameInput gameInput;
   [SerializeField] private LayerMask counterLayerMask;

   // Private fields
   private bool isWalking = false;
   private Vector3 lastInteractionDirection;
   private ClearCounter selectedCounter;

   // Public fields
   public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
   public class OnSelectedCounterChangedEventArgs: EventArgs {
      public ClearCounter selectedCounter;
   }
   
   // Unity Methods
   private void Awake() {
      if (Instance != null)
      {
         Debug.LogError("There is more than 1 Player instance");
      }
      Instance = this;
   }

   private void Start() {
      // Subscribe to the publisher of OnInteractAction event
      gameInput.OnInteractAction +=  GameInput_OnInteractEvent;
   }

   private void Update() 
   {
      HandleMovement();
      HandleInteractions();
   }

   // Private Methods
   private bool CanPlayerMove(Vector3 moveDirection, float moveDistance)
   {
      float playerHeight = 2f;
      float playerRadius = 0.7f;

      bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirection, moveDistance);
      return canMove;
   }

   private void HandleMovement()
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

   private void HandleInteractions()
   {
      Vector2 inputVector = gameInput.GetMovementVectorNormalised();

      // Calculate distance and direction for interaction
      float interactionDistance = 2f;
      Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

      // having lastInteractionDirection because move direction will be 0 if player is not moving, but it can still interact
      if (moveDirection != Vector3.zero)
      {
         lastInteractionDirection = moveDirection;
      }

      // Check if there is any object where the player is moving. out raycastHit will give the object with which the player can intersect
      if (Physics.Raycast(transform.position, lastInteractionDirection, out RaycastHit raycastHit, interactionDistance, counterLayerMask)) // using layerMask so that the raycast only hit the object on that layer
      {
         // Check if the gameObject has the ClearCount component
         if (raycastHit.transform.TryGetComponent<ClearCounter>(out ClearCounter clearCounter))
         {
            if (selectedCounter != clearCounter)
            {
               SetSelectedCounter(clearCounter);
            }
         }
         else
         {
            SetSelectedCounter(null);
         }
      }
      else
      {
         SetSelectedCounter(null);
      }
   }

   private void SetSelectedCounter(ClearCounter selectedCounter)
   {
      this.selectedCounter = selectedCounter;
    
      // Publish the event to update counters
      OnSelectedCounterChanged?.Invoke(
         this,
         new OnSelectedCounterChangedEventArgs {
            selectedCounter = selectedCounter
         }
      );
   }

   // Subscriber function of Interact event
   private void GameInput_OnInteractEvent(object sender, System.EventArgs eventArgs)
   {
      selectedCounter?.Interact();
   }

   // Public Methods
   public bool IsWalking()
   {
      return isWalking;
   }
}
