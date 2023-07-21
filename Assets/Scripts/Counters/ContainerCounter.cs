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
        // Create a kitchen object and give it to the player
        Transform kitechObjectTransform = Instantiate(kitchenObjectSO.prefab);
        kitechObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);

        // Publish the event
        OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
    }
}
