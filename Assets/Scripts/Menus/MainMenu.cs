using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu, controlsMenu;
    public void PlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainLevel");
    }

    public void ControlsOpen()
    {
        mainMenu.SetActive(false);
        controlsMenu.SetActive(true);
    }

    public void ControlsClosed()
    {
        mainMenu.SetActive(true);
        controlsMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
