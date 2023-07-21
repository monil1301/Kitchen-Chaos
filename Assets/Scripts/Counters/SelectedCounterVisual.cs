using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    // Serialized fields
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObjectArray;

    // Unity Methods
    private void Start() {
        // Subscribe to the publisher of OnSelectedCounterChanged event
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    // Private methods
    // Subscriber function of selected counter change event
    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs eventArgs)
    {
        foreach (var visualGameObject in visualGameObjectArray)
        {            
            visualGameObject.SetActive(eventArgs.selectedCounter == baseCounter);
        }
    }
}
