using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    // Serialized fields
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;

    // Unity Methods
    private void Awake() 
    {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start() 
    {
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeDelivered += DeliveryManager_OnRecipeDelivered;
    }

    // Private Methods
    private void UpdateVisual () {
        // Destroy the list
        foreach (Transform child in container) 
        {
            // Do not destroy the template used to create other visuals
            if (child == recipeTemplate) continue; 
            Destroy (child.gameObject);
        }

        // Create recipes from the template
        foreach (RecipeSO recipeSO in DeliveryManager. Instance.GetWaitingRecipeSOList ()) 
        {
            Transform recipeTransform = Instantiate (recipeTemplate, container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<RecipeTemplateUI>().SetRecipeSO(recipeSO);
        }
    }

    private void DeliveryManager_OnRecipeSpawned(object sender, EventArgs eventArgs)
    {
        UpdateVisual();
    }
    
    private void DeliveryManager_OnRecipeDelivered(object sender, EventArgs eventArgs)
    {
        UpdateVisual();
    }   
}
