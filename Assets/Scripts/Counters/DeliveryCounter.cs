using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    // Public fields
    public static DeliveryCounter Instance { get; private set; }

    // Unity Method
    private void Awake() 
    {
        Instance = this;        
    }

    // Public Methods
    public override void Interact(Player player)
    {
        // Check if the player has an kitchen object to deliver
        if (player.HasKitchenObject())
        {
            // Delivery can only be done with the plate, so check if the object with player is a plate or not
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
                player.GetKitchenObject().DestroySelf();
            }
        }
    }
}
