using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnWarningUI : MonoBehaviour
{
    // Serialized fields
    [SerializeField] private StoveCounter stoveCounter;

    // Unity Methods
    private void Start()
    {
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
        Hide();
    }

    // Private Methods
    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs eventArgs)
    {
        float burnShowProgressAmount = 0.3f; // Progress after which want to show burning warning

        // Check if it is fried and progress is greater than burnShowProgressAmount
        bool show = stoveCounter.IsFried() && eventArgs.progressNormalised > burnShowProgressAmount;

        if (show)
        {
            Show();
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
