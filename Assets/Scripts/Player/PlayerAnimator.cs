using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    // Constants
    private const string IS_WALKING = "IsWalking";

    // Serialized fields
    [SerializeField] private Player player;

    // Components
    private Animator animator;

    // Unity Methods
    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        animator.SetBool(IS_WALKING, player.IsWalking());
    }
}
