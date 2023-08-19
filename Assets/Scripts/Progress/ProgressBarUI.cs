using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    // Serialized fields
    [SerializeField] private Image barImage;
    [SerializeField] private GameObject hasProgressGameObject;

    // Private fields
    private IHasProgress hasProgress;

    // Unity Methods
    private void Start() {
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
        if (hasProgress == null)
            Debug.LogError("Game Object " + hasProgressGameObject + " does not have a component");

        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;
        barImage.fillAmount = 0f; // make it empty on start

        Hide(); // Hide it initially
    }

    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs eventArgs)
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
