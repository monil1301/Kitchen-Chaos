using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    // Serialized fields
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject visualGameObject;

    // Unity Methods
    private void Start() {
        // Subscribe to the publisher of OnSelectedCounterChanged event
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    // Private methods
    // Subscriber function of selected counter change event
    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs eventArgs)
    {
        visualGameObject.SetActive(eventArgs.selectedCounter == clearCounter);
    }
}
