using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStaticDataManager : MonoBehaviour
{
    // Serialized fields
    [SerializeField] private bool shouldResetOnAwake;

    // Unity Methods
    private void Awake()
    {
        if (shouldResetOnAwake)
        {
            ResetStaticData();
        }
    }

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        }
    }

    // Private Methods
    private void ResetStaticData()
    {
        // Clear static data
        TrashCounter.ResetStaticData();
        CuttingCounter.ResetStaticData();
        BaseCounter.ResetStaticData();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGameOver())
        {
            ResetStaticData();
        }
    }
}
