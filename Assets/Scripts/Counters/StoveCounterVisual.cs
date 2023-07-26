using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    // Serialized fields
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject stoveOnGameObject;
    [SerializeField] private GameObject particlesOnGameObject;

    // Unity Methods
    private void Start() 
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    // Private Methods
    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs eventArgs)
    {
        bool showVisual = eventArgs.state == StoveCounter.State.Frying || eventArgs.state == StoveCounter.State.Fried;
        stoveOnGameObject.SetActive(showVisual);
        particlesOnGameObject.SetActive(showVisual);
    }
}
