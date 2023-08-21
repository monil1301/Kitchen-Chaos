using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    // Serialized fields
    [SerializeField] private StoveCounter stoveCounter;

    // Private fields
    private AudioSource audioSource;

    // Unity Methods
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() 
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    // Private Methods
    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs eventArgs)
    {
        // Play sound only when something is frying or fried.
        bool playSound = eventArgs.state == StoveCounter.State.Frying || eventArgs.state == StoveCounter.State.Fried;
        if (playSound)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }
}
