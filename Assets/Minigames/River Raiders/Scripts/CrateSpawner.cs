using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateSpawner : MonoBehaviour
{
    public GameObject cratePrefab; // Crate prefab

    public Transform spawnPoint; // Crate spawn point

    public float spawnInterval = 5.0f; // Crate spawn interval is set to 5 seconds (between spawns)

    // Private variable to keep track of time
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the timer
        timer = spawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
        // Countdown timer
        timer -= Time.deltaTime;

        // Check if the timer has reached zero
        if (timer <= 0)
        {
            SpawnCrate(); // Spawn Crate function is called
            
            // Reset timer
            timer = spawnInterval;
        }
    }

    // Spawn Crate Function
    void SpawnCrate()
    {
        // Instantiate a new crate at the spawn point's position and rotation
        Instantiate(cratePrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
