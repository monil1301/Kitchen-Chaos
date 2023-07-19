using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{
    // Serialized fields
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;

    // Private fields
    private KitchenObject kitchenObject;

    // Public methods
    public void Interact(Player player)
    {
        // Check if any object is present on the counter
        if (kitchenObject == null)
        {
            // Create a kitchen object at the top of the counter
            Transform kitechObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            kitechObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        }
        else
        {
            // Player picks up the kitchen object
            kitchenObject.SetKitchenObjectParent(player);
        }
    }

    // Interface methods implementation
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }

     public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
