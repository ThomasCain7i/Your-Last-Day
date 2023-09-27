using UnityEngine;

public class SpinMenu : MonoBehaviour
{
    public float rotationSpeed = 30.0f; // Adjust the speed as needed

    void Update()
    {
        // Rotate the object around the Y-axis
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}