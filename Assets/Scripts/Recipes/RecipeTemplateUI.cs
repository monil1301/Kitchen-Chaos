using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecipeTemplateUI : MonoBehaviour
{
    // Serialized field
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;

    // Unity Methods
    private void Awake ()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    // Public Methods
    public void SetRecipeSO(RecipeSO recipeSO)
    {
        recipeNameText.text = recipeSO.recipeName;

        // Destroy the recipes
        foreach (Transform child in iconContainer)
        {
            // Do not destroy the template used to create other visuals
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }

        // Create icons from the template
        foreach (KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectSOList) 
        {
            Transform iconTransform = Instantiate (iconTemplate, iconContainer);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
        }
   }
}
