using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOptionsUI : MonoBehaviour
{
    // Serialized fields
    [SerializeField] private Transform rebindKeyTransform;
    [SerializeField] private Button soundButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI soundText;
    [SerializeField] private TextMeshProUGUI musicText;

    [Header("Keys Buttons")]
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAltButton;
    [SerializeField] private Button pauseButton;

    [Header("Keys Texts")]
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAltText;
    [SerializeField] private TextMeshProUGUI pauseText;

    [Header("Gamepad Buttons")]
    [SerializeField] private Button gamepadInteractButton;
    [SerializeField] private Button gamepadInteractAltButton;
    [SerializeField] private Button gamepadPauseButton;

    [Header("Gamepad Texts")]
    [SerializeField] private TextMeshProUGUI gamepadInteractText;
    [SerializeField] private TextMeshProUGUI gamepadInteractAltText;
    [SerializeField] private TextMeshProUGUI gamepadPauseText;

    // Public fields
    public static GameOptionsUI Instance;

    // Unity Methods
    private void Awake()
    {
        Instance = this;

        soundButton.onClick.AddListener(
            () =>
            {
                SoundManager.Instance.ChangeVolume();
                UpdateVisual();
            }
        );

        musicButton.onClick.AddListener(
            () =>
            {
                MusicManager.Instance.ChangeVolume();
                UpdateVisual();
            }
        );

        closeButton.onClick.AddListener(
            () =>
            {
                GamePauseUI.Instance.Show();
                Hide();
            }
        );

        AddKeyBindingButtonClickListeners();
    }

    private void Start()
    {
        UpdateVisual();

        Hide();
        HideRebindKeyUI();
    }

    // Private Methods
    private void UpdateVisual()
    {
        soundText.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);

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

    private void AddKeyBindingButtonClickListeners()
    {
        moveUpButton.onClick.AddListener(() => { RebindKey(KeyBinding.MoveUp); });
        moveDownButton.onClick.AddListener(() => { RebindKey(KeyBinding.MoveDown); });
        moveLeftButton.onClick.AddListener(() => { RebindKey(KeyBinding.MoveLeft); });
        moveRightButton.onClick.AddListener(() => { RebindKey(KeyBinding.MoveRight); });
        interactButton.onClick.AddListener(() => { RebindKey(KeyBinding.Interact); });
        interactAltButton.onClick.AddListener(() => { RebindKey(KeyBinding.InteractAlternate); });
        pauseButton.onClick.AddListener(() => { RebindKey(KeyBinding.Pause); });

        gamepadInteractButton.onClick.AddListener(() => { RebindKey(KeyBinding.Gamepad_Interact); });
        gamepadInteractAltButton.onClick.AddListener(() => { RebindKey(KeyBinding.Gamepad_InteractAlternate); });
        gamepadPauseButton.onClick.AddListener(() => { RebindKey(KeyBinding.Gamepad_Pause); });
    }

    private void RebindKey(KeyBinding keyBinding)
    {
        ShowRebindKeyUI();
        GameInput.Instance.RebindKey(keyBinding, () =>
        {
            UpdateVisual();
            HideRebindKeyUI();
        });
    }

    private void ShowRebindKeyUI()
    {
        rebindKeyTransform.gameObject.SetActive(true);
    }

    private void HideRebindKeyUI()
    {
        rebindKeyTransform.gameObject.SetActive(false);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    // Public Methods
    public void Show()
    {
        gameObject.SetActive(true);
    }
}
