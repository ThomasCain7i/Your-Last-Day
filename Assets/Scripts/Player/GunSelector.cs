using System.Collections;
using System.Collections.Generic;
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
                machineGun.SetActive(true);
                shotGun.SetActive(false);
                pistolGun.SetActive(false);
                railGun.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                machineGun.SetActive(false);
                shotGun.SetActive(true);
                pistolGun.SetActive(false);
                railGun.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                machineGun.SetActive(false);
                shotGun.SetActive(false);
                pistolGun.SetActive(true);
                railGun.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                machineGun.SetActive(false);
                shotGun.SetActive(false);
                pistolGun.SetActive(false);
                railGun.SetActive(true);
            }
        }
    }
}
