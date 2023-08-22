using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    // Serialized fields
    [SerializeField] private RecipeListSO recipeListSO;

    // Private fields
    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer = 0;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipeCountMax = 4;
    private int successfulRecipesDeliveredCount = 0;

    // Public fields
    public static DeliveryManager Instance { get; private set; }
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeDelivered;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailure;

    // Unity Methods
    private void Awake() 
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update() 
    {
        spawnRecipeTimer += Time.deltaTime; // Update timer to spawn a recipe

        // Check for timer completion to spawn a recipe
        if (spawnRecipeTimer >= spawnRecipeTimerMax)
        {
            spawnRecipeTimer = 0; // reset the timer

            // Do not spawn new recipe if the waiting list has reached the max limit
            if (waitingRecipeSOList.Count < waitingRecipeCountMax)
            {
                // Spawn a random recipe from the list of recipes and add to waiting list
                RecipeSO recipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
                waitingRecipeSOList.Add(recipeSO);
                OnRecipeSpawned?.Invoke(this, EventArgs.Empty); 
            }
        }
    }

    // Public Methods
    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        // Loop through the waiting recipe list to match the recipe delivered by the player
        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            // Check if ingredients count match, then only proceed 
            if (waitingRecipeSO.kitchenObjectSOList.Count== plateKitchenObject.GetKitchenObjectSOList().Count) 
            {
                bool plateContentsMatchesRecipe = true;

                // Loop through all ingredients in the Recipe
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList) 
                {
                    bool ingredientFound = false;

                    // Loop through all ingredients in the Plate
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList()) 
                    {
                        // Check if the ingredient matches
                        if (plateKitchenObjectSO == recipeKitchenObjectSO) 
                        {
                            ingredientFound = true;
                            break;
                        }
                    }

                    // If ingredient not found then this plate contents do not match this recepi
                    if (!ingredientFound) {
                        plateContentsMatchesRecipe = false;
                    }
                }
                
                // Player delivered the correct recipe! 
                if (plateContentsMatchesRecipe) {
                    successfulRecipesDeliveredCount++;
                    waitingRecipeSOList.RemoveAt(i); // Remove from the waiting list as the recipe is delivered
                    OnRecipeDelivered?.Invoke(this, EventArgs.Empty); 
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty); 
                    return;
                }
            }
        }

        // Player delivered wrong recipe. No match found 
        OnRecipeFailure?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSO> GetWaitingRecipeSOList() {
        return waitingRecipeSOList;
    }

    public int GetSuccessfulRecipesDeliveredCount()
    {
        return successfulRecipesDeliveredCount;
    }
}
