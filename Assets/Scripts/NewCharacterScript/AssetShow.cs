using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetShow : MonoBehaviour
{
    public CircleCollider2D circleCollider; // Reference to the CircleCollider2D on the player
    public GameObject assetToShowPrefab; // Assign the asset prefab to show in the Unity Editor
    public float showDuration = 0.1f; // Duration to show the asset in seconds

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with an enemy and involves the circle collider
        if (collision.gameObject.CompareTag("Enemy") && collision.collider == circleCollider)
        {
            // Instantiate the asset prefab at the collision point
            GameObject assetToShow = Instantiate(assetToShowPrefab, collision.contacts[0].point, Quaternion.identity);

            // Start coroutine to show the asset for the specified duration
            StartCoroutine(ShowAssetForDuration(assetToShow));
        }
    }

    IEnumerator ShowAssetForDuration(GameObject assetToShow)
    {
        // Enable the asset
        assetToShow.SetActive(true);

        // Wait for the specified duration
        yield return new WaitForSeconds(showDuration);

        // Disable the asset
        assetToShow.SetActive(false);
    }
}
