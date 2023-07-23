using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    // Serialized fields
    [SerializeField] private Transform counterTopPoint;

    // Private fields
    private KitchenObject kitchenObject;

    // Public methods
    public virtual void Interact(Player player)
    {

    }

    public virtual void InteractAlternate(Player player)
    {

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
