using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    // Serialized fields
    [SerializeField] private TextMeshProUGUI recipesDeliveredCountText;

     // Unity Methods
    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        Hide(); // Hide the countdown on start
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGameOver())
        {
            Show();
            recipesDeliveredCountText.text = DeliveryManager.Instance.GetSuccessfulRecipesDeliveredCount().ToString();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
