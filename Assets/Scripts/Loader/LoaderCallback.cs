using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallback : MonoBehaviour
{
    // Private fields
    private bool isFirstFrame = true;

    // Unity Methods
    private void Update()
    {
        // isFirstFrame used to only call this code once
        if (isFirstFrame)
        {
            isFirstFrame = false;

            // calling loader callback so that the actual target scene starts to load
            Loader.LoaderCallback();
        }
    }
}
