using UnityEngine;
public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    [SerializeField]
    private GameObject deathMenu;

    void Start()
    {
        //Set urrent health it max health
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
        Cursor.lockState = CursorLockMode.Confined;
    }
}