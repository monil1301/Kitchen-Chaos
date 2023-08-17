using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenobjectSO_Gameobject 
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }

    // Serialized fields
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitchenobjectSO_Gameobject> kitchenobjectSOGameobjectList;

    // Unity Methods
    private void Start() 
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
    }

    // Private Methods
    private void PlateKitchenObject_OnIngredientAdded(System.Object sender, PlateKitchenObject.OnIngredientAddedEventArgs eventArgs)
    {
        foreach (var item in kitchenobjectSOGameobjectList)
        {
            if (eventArgs.kitchenObjectSO == item.kitchenObjectSO)
            {
                item.gameObject.SetActive(true);
            }
        }
    }
}
