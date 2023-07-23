using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    // Constants
    private const string CUT = "Cut";

    // Serialized fields
    [SerializeField] private CuttingCounter cuttingCounter;

    // Private fields
    private Animator animator;

    // Unity methods
    private void Awake() 
    {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        cuttingCounter.OnCut += CuttingCounter_OnCut;
    }

    private void CuttingCounter_OnCut(object sender, EventArgs eventArgs)
    {
        animator.SetTrigger(CUT);
    }
}
