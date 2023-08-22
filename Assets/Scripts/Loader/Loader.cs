using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    // Private fields
    private static Scene targetScene;

    // Public Methods
    public static void Load(Scene targetScene)
    {
        // save the target scene and load the loading scene
        Loader.targetScene = targetScene;
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    public static void LoaderCallback()
    {
        // Load target scene once the loading scene is loaded
        SceneManager.LoadScene(targetScene.ToString());
    }
}