using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    // Serialized fields
    [Header("Keys Texts")]
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAltText;
    [SerializeField] private TextMeshProUGUI pauseText;


    [Header("Gamepad Texts")]
    [SerializeField] private TextMeshProUGUI gamepadInteractText;
    [SerializeField] private TextMeshProUGUI gamepadInteractAltText;
    [SerializeField] private TextMeshProUGUI gamepadPauseText;

    // Unity Methods
    private void Start()
    {
        GameInput.Instance.OnRebindKey += GameInput_OnRebindKey;
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        UpdateVisual();
    }

    // Private Methods
    private void UpdateVisual()
    {
        // Show binding of the keys
        moveUpText.text = GameInput.Instance.GetKeyBinding(KeyBinding.MoveUp);
        moveDownText.text = GameInput.Instance.GetKeyBinding(KeyBinding.MoveDown);
        moveLeftText.text = GameInput.Instance.GetKeyBinding(KeyBinding.MoveLeft);
        moveRightText.text = GameInput.Instance.GetKeyBinding(KeyBinding.MoveRight);
        interactText.text = GameInput.Instance.GetKeyBinding(KeyBinding.Interact);
        interactAltText.text = GameInput.Instance.GetKeyBinding(KeyBinding.InteractAlternate);
        pauseText.text = GameInput.Instance.GetKeyBinding(KeyBinding.Pause);

        // Show binding of the gamepad keys
        gamepadInteractText.text = GameInput.Instance.GetKeyBinding(KeyBinding.Gamepad_Interact);
        gamepadInteractAltText.text = GameInput.Instance.GetKeyBinding(KeyBinding.Gamepad_InteractAlternate);
        gamepadPauseText.text = GameInput.Instance.GetKeyBinding(KeyBinding.Gamepad_Pause);
    }

    private void GameInput_OnRebindKey(object sender, System.EventArgs eventArgs)
    {
        UpdateVisual();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsCountdownToStartActive())
        {
            Hide();
        }
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
