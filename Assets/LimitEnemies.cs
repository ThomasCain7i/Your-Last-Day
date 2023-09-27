using UnityEngine;

public class LimitEnemies : MonoBehaviour
{
    public string targetTag = "Enemy"; // Set the tag you want to target in the Unity Inspector
    public int maxObjects; // Set the maximum number of objects before destruction

    private void Update()
    {
        // Find all GameObjects with the specified tag
        GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag(targetTag);

        // Check if the number of objects with the tag exceeds the maximum
        if (objectsToDestroy.Length > maxObjects)
        {
            // Destroy the excess objects starting from the oldest ones
            for (int i = 0; i < objectsToDestroy.Length - maxObjects; i++)
            {
                Destroy(objectsToDestroy[i]);
            }
        }
    }
}
