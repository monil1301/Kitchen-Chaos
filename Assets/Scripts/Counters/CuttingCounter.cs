using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{ 
    // Public methods
    public override void Interact(Player player)
    {
        // Check if the counter has kitchen object or not
        if (!HasKitchenObject())
        {
            // Counter does not have anything, so check if player has kitchen object to place on the counter
            if (player.HasKitchenObject())
            {   
                // Place the kitchen object from player on the counter
                player.GetKitchenObject().SetKitchenObjectParent(this);
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
        // Check if the counter has kitchen object or not
        if (HasKitchenObject())
        {
            // Counter has a kitchen object, so destory it and spawn the sliced or cooked kitchen object
            GetKitchenObject().DestroySelf();
        }
    }
}
