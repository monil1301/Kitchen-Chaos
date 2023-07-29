using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    // Constants
    private const float SPAWN_PLATE_TIMER_MAX = 4f;
    private const int PLATES_SPAWNED_COUNT_MAX = 4;

    // Serialized fields
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;

    // Private fields
    private float spawnPlateTimer;
    private int platesSpawnedCount;

    // Public fields
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    // Unity Methods
    private void Update() 
    {
        // Timer to spawn plate after a fixed interval
        spawnPlateTimer += Time.deltaTime;

        // Check if timer has crossed the spawn interval
        if (spawnPlateTimer > SPAWN_PLATE_TIMER_MAX)
        {
            spawnPlateTimer = 0f; // reset timer

            // Only spawn the plate if it is less then a fixed count
            if (platesSpawnedCount < PLATES_SPAWNED_COUNT_MAX)
            {
                platesSpawnedCount++;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty); // Event to generate a dummy visual for the plate
            }
        }
    }

    // Public Methods
    public override void Interact(Player player)
    {
        // Check if player does not have anything and can take plate from the container
        if (!player.HasKitchenObject())
        {
            // Check if the plates are present on the counter or not
            if (platesSpawnedCount > 0)
            {
                // Spawn an actual plate and give it to the player
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                OnPlateRemoved?.Invoke(this, EventArgs.Empty); // Event to destory the dummy visual for the plate
                platesSpawnedCount--;
            }
        }
    }
}
