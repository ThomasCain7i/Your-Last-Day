using UnityEngine;

public class Gun : MonoBehaviour
{
    public float rayLength = 50f;
    [SerializeField] private Transform cam;

    void Update()
    {
        Debug.DrawRay(transform.position, cam.forward, Color.green);

        // Cast a ray from the position of this GameObject's transform.forward
        Ray ray = new Ray(transform.position, transform.forward);

        RaycastHit hitInfo;

        // Perform the raycast
        if (Physics.Raycast(cam.position, cam.forward, out hitInfo, rayLength))
        {
            // Check if the hit object has the "Enemy" tag
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                // Do something when the ray hits an object with the "Enemy" tag
                Debug.Log("Hit an enemy!");
            }
        }
    }
}