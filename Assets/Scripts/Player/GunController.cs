using UnityEngine;

public class GunController : MonoBehaviour
{
    public int damage = 10;
    public float range = 100f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Change "Fire1" to the input you want to use for shooting.
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            // Check if the hit object has a Health script or component.
            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                // Apply damage to the hit object.
                enemyHealth.TakeDamage(damage);
            }
        }

        // Visualize the raycast for debugging purposes.
        Debug.DrawRay(transform.position, transform.forward * range, Color.red, 0.1f);
    }
}
