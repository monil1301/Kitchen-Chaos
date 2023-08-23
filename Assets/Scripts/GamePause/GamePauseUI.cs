using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    // Serialized fields
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;

    // Unity Methods
    private void Awake()
    {
        resumeButton.onClick.AddListener(
            () =>
            {
                GameManager.Instance.ToggleGamePause();
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

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
