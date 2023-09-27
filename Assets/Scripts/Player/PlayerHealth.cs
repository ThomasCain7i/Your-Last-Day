using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int maxHealth = 100;
    public int currentHealth;

    [SerializeField] private GameObject deathMenu, ammoMenu;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Time.timeScale = 0f;
        deathMenu.SetActive(true);
        ammoMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
    }
}
