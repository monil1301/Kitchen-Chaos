using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    // Serialized fields
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    // Private fields
    private ClearCounter clearCounter;

    // Public methods
    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }

    public void SetClearCounter(ClearCounter clearCounter)
    {
        if (this.clearCounter != null)
        {
            this.clearCounter.ClearKitchenObject();
        }

        if (clearCounter.HasKitchenObject())
        {
            Debug.LogError("Counter already has a object");
        }

        this.clearCounter = clearCounter;
        clearCounter.SetKitchenObject(this);

        // Set the counter's top point as the parent of the kitchen object
        transform.parent = clearCounter.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public ClearCounter GetClearCounter()
    {
        return clearCounter;
    }
}
