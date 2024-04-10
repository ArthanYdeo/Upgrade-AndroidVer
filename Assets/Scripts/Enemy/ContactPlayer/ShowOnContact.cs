using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOnContact : MonoBehaviour
{
    public GameObject assetToShowPrefab; // Assign the asset prefab to show in the Unity Editor
    public float showDuration = 0.1f; // Duration to show the asset in seconds


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Instantiate the asset prefab at the trigger's position
            GameObject assetToShow = Instantiate(assetToShowPrefab, transform.position, Quaternion.identity);

            // Show the asset for the specified duration
            StartCoroutine(ShowAssetForDuration(assetToShow));
            
        }
    }
  /*  private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Instantiate the asset prefab at the trigger's position
            GameObject assetToShow = Instantiate(assetToShowPrefab, transform.position, Quaternion.identity);

            // Show the asset for the specified duration
            StartCoroutine(ShowAssetForDuration(assetToShow));
        }
    }*/

    IEnumerator ShowAssetForDuration(GameObject assetToShow)
    {
        assetToShow.SetActive(true);
        // Wait for the specified duration
        yield return new WaitForSeconds(showDuration);

        // Destroy the asset after the duration
        Destroy(assetToShow);
        assetToShow.SetActive(false);
    }
}
