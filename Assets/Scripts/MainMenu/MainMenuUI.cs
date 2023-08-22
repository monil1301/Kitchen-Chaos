using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
                SceneManager.LoadScene(1);
            }
        );

        quitButton.onClick.AddListener(
            () => {
                Application.Quit();
            }
        );
    }
}
