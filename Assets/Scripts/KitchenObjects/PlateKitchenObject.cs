using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    // Serialized fields
    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;

    // Private fields
    private List<KitchenObjectSO> kitchenObjectSOList;

    // Public fields
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs {
        public KitchenObjectSO kitchenObjectSO;
    }

    // Unity Methods
    private void Awake() 
    {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }

    // Public Methods
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO) {
        // Do not kitchen object to the plate if it is not valid
        if (!validKitchenObjectSOList.Contains(kitchenObjectSO))
        {
            return false;
        }
        // Check if the kitchen is already present on plate
        if (kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            // Do not add if the object already exists
            return false;
        }
        else
        {
            // Add object to plate 
            kitchenObjectSOList.Add(kitchenObjectSO);
            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs { kitchenObjectSO = kitchenObjectSO });
            return true;
        }
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return kitchenObjectSOList;
    }
}
