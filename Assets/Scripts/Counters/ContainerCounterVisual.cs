using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    // Constants
    private const string OPEN_CLOSE = "OpenClose";

    // Serialized fields
    [SerializeField] private ContainerCounter containerCounter;

    // Private fields
    private Animator animator;

    // Unity methods
    private void Awake() 
    {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        containerCounter.OnPlayerGrabbedObject += ContainerCounter_OnPlayerGrabbedObject;
    }

    private void ContainerCounter_OnPlayerGrabbedObject(System.Object sender, System.EventArgs eventArgs)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
