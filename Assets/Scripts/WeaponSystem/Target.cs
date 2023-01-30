using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IDamageable
{

    public float health;

    public void TakeDamage(float damage)
    {
        //When health reaches 0 destroy object
        health -= damage;
        if (health <= 0) Destroy(gameObject);
    }
}
