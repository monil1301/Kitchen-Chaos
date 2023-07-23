using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    // Serialized fields
    [SerializeField] private Image barImage;
    [SerializeField] private CuttingCounter cuttingCounter;

    // Unity Methods
    private void Start() {
        cuttingCounter.OnProgressChanged += CuttingCounter_OnProgressChanged;
        barImage.fillAmount = 0f; // make it empty on start

        Hide(); // Hide it initially
    }

    private void CuttingCounter_OnProgressChanged(object sender, CuttingCounter.OnProgressChangedEventArgs eventArgs)
    {
        barImage.fillAmount = eventArgs.progressNormalised;

        if (eventArgs.progressNormalised == 0 || eventArgs.progressNormalised == 1)
        {
            Hide();
        }
        else
        {
            Show();
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
