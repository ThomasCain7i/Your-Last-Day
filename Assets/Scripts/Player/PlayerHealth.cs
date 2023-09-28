using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int maxHealth = 100;
    public int currentHealth;

    public TextMeshProUGUI healthText;

    [SerializeField] private GameObject deathMenu, ammoMenu;
    [SerializeField] private PauseMenu pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthText.SetText("Health: " + currentHealth);

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
