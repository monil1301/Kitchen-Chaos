using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    // Serialized fields
    [SerializeField] private StoveCounter stoveCounter;

    // Private fields
    private AudioSource audioSource;
    private bool playWarningSound;
    private float warningSoundTimer;

    // Unity Methods
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
    }

    private void Update()
    {
        if (playWarningSound)
        {
            warningSoundTimer -= Time.deltaTime;
            if (warningSoundTimer <= 0f)
            {
                float warningSoundTimerMax = 0.2f; // plays the sound 5 times a second
                warningSoundTimer = warningSoundTimerMax;

                SoundManager.Instance.PlayWarningSound(stoveCounter.transform.position);
            }
        }
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

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs eventArgs)
    {
        float burnShowProgressAmount = 0.3f; // Progress after which want to play burning warning sound

        // Check if it is fried and progress is greater than burnShowProgressAmount
        playWarningSound = stoveCounter.IsFried() && eventArgs.progressNormalised > burnShowProgressAmount;
    }

}
