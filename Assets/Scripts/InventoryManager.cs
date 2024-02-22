using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
      public static InventoryManager Instance;

    private List<string> inventory = new List<string>(); // List to store collected items

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist the inventory manager between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddItem(string itemName)
    {
        inventory.Add(itemName);
    }

    public bool HasItem(string itemName)
    {
        return inventory.Contains(itemName);
    }
}