using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;

   // Public methods
   public void Interact()
   {
        // Create a kitchen object at the top of the counter
        Transform kitechObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
        kitechObjectTransform.localPosition = Vector3.zero;

        Debug.Log(kitechObjectTransform.GetComponent<KitchenObject>().GetKitchenObjectSO().objectName);
   }
}
