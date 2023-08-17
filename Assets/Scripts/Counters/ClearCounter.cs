using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
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
            else
            {
                // Check if the player is carrying a plate
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    // Add the item on the counter on to the plate of the player
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf(); // Destory if added to the plate
                    }
                }
                else
                {
                    // Player is not carrying a plate but some other object, so put in on the plate if it is on the counter
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        // Add the item from the counter on to the plate on the couter
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf(); // Destory if added to the plate
                        }
                    }
                }
            }
        }
    }
}
