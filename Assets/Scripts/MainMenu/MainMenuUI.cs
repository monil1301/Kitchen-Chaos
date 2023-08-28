using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    // Serialized fields
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    // Unity Methods
    private void Awake()
    {
        // Show selected and then you can use gamepad to navigate buttons
        playButton.Select();

        playButton.onClick.AddListener(
            () =>
            {
                Loader.Load(Scene.GameScene);
            }
        );

        quitButton.onClick.AddListener(
            () =>
            {
                Application.Quit();
            }
        );

        // To start the time after coming to main menu from the pause menu as we pause everything using time
        Time.timeScale = 1f;
    }
}
