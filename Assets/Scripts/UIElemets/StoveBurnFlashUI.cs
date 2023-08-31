using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnFlashUI : MonoBehaviour
{
    // Constants
    private const string IS_FLASHING = "IsFlashing";

    // Serialized fields
    [SerializeField] private StoveCounter stoveCounter;

    // Private fields
    private Animator animator;

    // Unity methods
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Unity Methods
    private void Start()
    {
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
        animator.SetBool(IS_FLASHING, false);
    }

    // Private Methods
    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs eventArgs)
    {
        float burnShowProgressAmount = 0.3f; // Progress after which want to show burning warning

        // Check if it is fried and progress is greater than burnShowProgressAmount
        bool show = stoveCounter.IsFried() && eventArgs.progressNormalised > burnShowProgressAmount;

        animator.SetBool(IS_FLASHING, show);
    }
}
