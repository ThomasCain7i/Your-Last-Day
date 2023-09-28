using UnityEngine;

public class GunSelector : MonoBehaviour
{
    [SerializeField] private GameObject machineGun, shotGun, pistolGun, railGun;

    [SerializeField] private PauseMenu pauseMenu;

    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu.GameIsPaused)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                MachineGun();
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ShotGun();
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Pistol();
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                RailGun();
            }
        }
    }

    private void MachineGun()
    {
        machineGun.SetActive(true);
        shotGun.SetActive(false);
        pistolGun.SetActive(false);
        railGun.SetActive(false);
    }

    private void ShotGun()
    {
        machineGun.SetActive(false);
        shotGun.SetActive(true);
        pistolGun.SetActive(false);
        railGun.SetActive(false);
    }

    private void Pistol()
    {
        machineGun.SetActive(false);
        shotGun.SetActive(false);
        pistolGun.SetActive(true);
        railGun.SetActive(false);
    }

    private void RailGun()
    {
        machineGun.SetActive(false);
        shotGun.SetActive(false);
        pistolGun.SetActive(false);
        railGun.SetActive(true);
    }
}
