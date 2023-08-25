using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOptionsUI : MonoBehaviour
{
    // Serialized fields
    [SerializeField] private Button soundButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI soundText;
    [SerializeField] private TextMeshProUGUI musicText;

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
    }

    private void Start()
    {
        UpdateVisual();

        Hide();
    }

    // Private Methods
    private void UpdateVisual()
    {
        soundText.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);
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
