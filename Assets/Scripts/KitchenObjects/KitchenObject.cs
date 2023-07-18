using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    // Serialized fields
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    // Public methods
    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }
}
