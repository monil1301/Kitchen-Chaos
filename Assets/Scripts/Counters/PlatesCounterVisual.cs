using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    // Serialized fields
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;
    [SerializeField] private PlatesCounter platesCounter;

    // Private fields
    private float plateOffsetY = 0.1f;
    private List<GameObject> platesGameObjectList;

    // Unity Methods
    private void Awake() 
    {
        platesGameObjectList = new List<GameObject>();
    }

    private void Start() 
    {
        platesCounter.OnPlateSpawned += PlatesCounter_OnPlatesSpawned;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    // Private Methods
    private void PlatesCounter_OnPlatesSpawned(object sender, EventArgs eventArgs)
    {
        // Spawn a plate
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);

        // Set the y position of the plate spawned based on the number of plates present on the counter
        plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * platesGameObjectList.Count, 0);

        // List to maintain the plates
        platesGameObjectList.Add(plateVisualTransform.gameObject);
    }

    private void PlatesCounter_OnPlateRemoved(object sender, EventArgs eventArgs)
    {
        // Remove the topmost plate 
        GameObject plateGameObject = platesGameObjectList[platesGameObjectList.Count - 1];
        platesGameObjectList.Remove(plateGameObject);
        Destroy(plateGameObject);
    }
}
