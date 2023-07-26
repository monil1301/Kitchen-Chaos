using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter
{
    // Serialized fields
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;

    // Private fields
    private float fryingTimer;
    private State currentState;
    private FryingRecipeSO fryingRecipeSO;

    // Public fields
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs 
    {
        public State state;
    }
    public enum State 
    {
        Idle,
        Frying,
        Fried,
        Burned
    }

    // Unity Methods
    private void Start()
    {
        currentState = State.Idle;
    }

    private void Update() 
    {
        if (HasKitchenObject()) 
        {
            switch (currentState)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    HandleFrying(
                        () => {  
                            // Update state
                            currentState = State.Fried;
                            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{ 
                                state = currentState 
                            });
                        }
                    );
                    break;
                case State.Fried:
                    HandleFrying(
                        () => {  
                            // Update state
                            currentState = State.Burned; 
                            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{ 
                                state = currentState 
                            });
                        }
                    );
                    break;
                case State.Burned:
                    break;
            }
        }
    }

    // Private Methods
    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        // Get frying recipe that matches the input
        foreach (var fryingRecipeSO in fryingRecipeSOArray)
        {
            if (fryingRecipeSO.input == inputKitchenObjectSO)
            {
                return fryingRecipeSO;
            }
        }

        return null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        // Get processed kitchen object of a kitchen object. Eg: Get cooked meat from uncooked meat
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.output;
        }

        return null;
    }

    private bool HasRecipeForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        // Check if the kitchen object can be processed ot not. Eg: Uncooked meat to cooked meat
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSO != null;
    }

    private void HandleFrying(Action onFried)
    {
        fryingTimer += Time.deltaTime; // Increase frying timer

        // Check if the meat is fried
        if (fryingTimer > fryingRecipeSO.fryingTimerMax) 
        {
            // Destroy uncooked meat and spawn the cooked meat
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);

            // Update the recipe and timer
            fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
            fryingTimer = 0f;
            onFried();
        }
    }

    // Public Methods
    public override void Interact(Player player)
    {
        // Check if the counter has kitchen object or not
        if (!HasKitchenObject())
        {
            // Counter does not have anything, so check if player has kitchen object to place on the counter
            if (player.HasKitchenObject())
            {
                // Only place the object if it can be processed
                if (HasRecipeForInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    // Place the kitchen object from player on the counter
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                    // Get recipe for frying and set the state
                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    currentState = State.Frying;
                    fryingTimer = 0f;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{ 
                                state = currentState 
                            });
                }
            }
        }
        else
        {
            // Counter has a kitchen object, so check if player does not have anything and can take object from the counter
            if (!player.HasKitchenObject())
            {
                // Player takes the kitchen object from the counter
                GetKitchenObject().SetKitchenObjectParent(player);

                // Reset the recipe, timer and state
                fryingRecipeSO = null;
                currentState = State.Idle;
                fryingTimer = 0f;
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{ 
                            state = currentState 
                        });
            }
        }
    }
}
