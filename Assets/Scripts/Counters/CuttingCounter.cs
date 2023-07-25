using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{ 
    // Serialized fields
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    // Private fields
    private int cuttingProgress;

    // Public fields
    public event EventHandler OnCut;
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs: EventArgs
    {
        public float progressNormalised;
    }

    // Private Methods
    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        // Get cutting recipe that has the input
        foreach (var cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return cuttingRecipeSO;
            }
        }

        return null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        // Get processed kitchen object of a kitchen object. Eg: Get tomoto slices from tomato
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        if (cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.output;
        }

        return null;
    }

    private bool HasRecipeForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        // Check if the kitchen object can be processed ot not. Eg: Tomato to tomoto slices
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        return cuttingRecipeSO != null;
    }

    // Public methods
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

                    // Update cutting progress to 0
                    cuttingProgress = 0;
                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs{
                        progressNormalised = (float) cuttingProgress / cuttingRecipeSO.cuttingProgressMax
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
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        // Check if the counter has kitchen object and only interact if it can be processed
        if (HasKitchenObject() && HasRecipeForInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
            OnCut?.Invoke(this, EventArgs.Empty);

            // Update cutting progress
            cuttingProgress++;
            OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs{
                progressNormalised = (float) cuttingProgress / cuttingRecipeSO.cuttingProgressMax
            });


            // Check if the cutting is done, then spawn the slices
            if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax) 
            {
                // Get the processed kitchen object for a kitchen object
                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());

                // Destory kitchen object and spawn the sliced kitchen object
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }
        }
    }
}
