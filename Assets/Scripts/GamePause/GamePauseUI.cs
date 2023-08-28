using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    // Serialized fields
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button mainMenuButton;

    // Public fields
    public static GamePauseUI Instance;

    // Unity Methods
    private void Awake()
    {
        Instance = this;
        resumeButton.onClick.AddListener(
            () =>
            {
                GameManager.Instance.ToggleGamePause();
            }
        );

        optionsButton.onClick.AddListener(
            () =>
            {
                GameOptionsUI.Instance.Show();
                Hide();
            }
        );

        mainMenuButton.onClick.AddListener(
            () =>
            {
                Loader.Load(Scene.MainMenuScene);
            }
        );
    }

    private void Start()
    {
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;

        Hide();
    }

    // Private Methods
    private void GameManager_OnGamePaused(object sender, System.EventArgs e)
    {
        Show();
    }

    private void GameManager_OnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    // Public Methods
    public void Show()
    {
        gameObject.SetActive(true);

        // Show selected and then you can use gamepad to navigate buttons
        resumeButton.Select();
    }
}
