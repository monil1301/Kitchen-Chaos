using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    // Serialized fields
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    // Public fields
    public event EventHandler OnPlayerGrabbedObject;
   
    // Public methods
    public override void Interact(Player player)
    {
        // Check if player does not have anything and can take object from the container
        if (!player.HasKitchenObject())
        {
            // Publish the event
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
            
            // Create a kitchen object and give it to the player
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
        }
    }
}
