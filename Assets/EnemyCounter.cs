using UnityEngine;
using TMPro;

public class EnemyCounter : MonoBehaviour
{
    public TextMeshProUGUI enemyText;

    [SerializeField] private PauseMenu pauseMenu;

    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu.GameIsPaused)
        {
            // Find all objects with the "Enemy" tag and store them in an array.
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            // Get the number of enemies.
            int enemyCount = enemies.Length;

            // Set the TextMeshProUGUI text to display the enemy count.
            enemyText.text = "Enemies Alive: " + enemyCount;
        }
    }
}
