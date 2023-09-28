using UnityEngine;

public class Destroy : MonoBehaviour
{
    public float delay = 5.0f; // Time in seconds before the object is destroyed

    void Start()
    {
        // Invoke the DestroyObject function after 'delay' seconds
        Invoke("DestroyObject", delay);
    }

    void DestroyObject()
    {
        // Destroy the game object this script is attached to
        Destroy(gameObject);
    }
}
