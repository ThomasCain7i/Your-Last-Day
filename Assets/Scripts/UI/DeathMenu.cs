using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject deathMenu;
    public void Retry()
    {
        Cursor.lockState = CursorLockMode.Locked;
        deathMenu.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainLevel");
    }

    public void RTM()
    {
        Cursor.lockState = CursorLockMode.Confined;
        deathMenu.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
