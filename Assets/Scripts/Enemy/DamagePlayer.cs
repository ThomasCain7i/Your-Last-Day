using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    private HealthSystem healthSystem;
    [SerializeField]
    private int damage;

    // Start is called before the first frame update
    void Start()
    {
        healthSystem = FindObjectOfType<HealthSystem>();
        damage = Random.Range(15, 20);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") // Check if the collider's tag is "Player"
        {
            Debug.Log("Player touched the enemy projectile");
            other.gameObject.GetComponent<HealthSystem>().TakeDamage(damage);
        }
    }
}
