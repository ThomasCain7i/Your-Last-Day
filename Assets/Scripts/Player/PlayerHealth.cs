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

    private void Update()
    {
        if (!pauseMenu.GameIsPaused)
        {

            //SetText
            healthText.SetText("Health: " + currentHealth);
        }
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
