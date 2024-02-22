using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
   public string[] collectibleNames; // Array to store names of collectible items
    public Vector3 spawnAreaSize = new Vector3(10f, 0f, 10f); // Size of the spawn area

    private void Start()
    {
        // Randomly select a position within the spawn area
        Vector3 randomPosition = new Vector3(
            Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2f),
            transform.position.y,
            Random.Range(-spawnAreaSize.z / 2f, spawnAreaSize.z / 2f)
        );

        // Set the position of the collectible to the randomly selected position
        transform.position = randomPosition;

        // Randomly select a collectible item name from the array
        string itemName = collectibleNames[Random.Range(0, collectibleNames.Length)];
        gameObject.name = itemName; // Set the GameObject's name to the selected item name
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collect(); // Call the Collect method when the player collides with the collectible
        }
    }

    private void Collect()
    {
        // Add the collected item to the inventory
        InventoryManager.Instance.AddItem(gameObject.name);

        // Disable the collectible object
        gameObject.SetActive(false);
    }
}
