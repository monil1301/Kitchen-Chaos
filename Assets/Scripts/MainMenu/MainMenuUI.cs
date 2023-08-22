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
        playButton.onClick.AddListener(
            () => {
                Loader.Load(Scene.GameScene);
            }
        );

        quitButton.onClick.AddListener(
            () => {
                Application.Quit();
            }
        );
    }
}
