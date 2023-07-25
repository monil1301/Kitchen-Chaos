using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    // Private methods
    private IEnumerator DestroyKitchenObjectWithAnimation(Transform kitchenObjectTransform, float animationDuration)
    {
        Vector3 initialScale = kitchenObjectTransform.localScale;
        Vector3 targetScale = Vector3.zero;
        float elapsedTime = 0f;

        // Scale down the kitchen object so that looks like putting in trash
        while (elapsedTime < animationDuration)
        {
            kitchenObjectTransform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final scale is exactly the target scale
        kitchenObjectTransform.localScale = targetScale;

        // Destroy the kitchen object so other objects can be put in trash
        GetKitchenObject().DestroySelf();
    }

   // Public methods
    public override void Interact(Player player)
    {
        // Check if the player has kitchen object or not
        if (player.HasKitchenObject())
        {   
            // Place the kitchen object from player on the counter
            player.GetKitchenObject().SetKitchenObjectParent(this);

            // Destroy the kitchen object with animation so it looks like put in a trash
            StartCoroutine(DestroyKitchenObjectWithAnimation(GetKitchenObject().transform, 0.2f));
        }
    }
}
