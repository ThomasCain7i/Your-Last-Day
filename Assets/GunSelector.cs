using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSelector : MonoBehaviour
{
    [SerializeField] private GameObject machineGun, shotGun, pistolGun;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            machineGun.SetActive(true);
            shotGun.SetActive(false);
            pistolGun.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            machineGun.SetActive(false);
            shotGun.SetActive(true);
            pistolGun.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            machineGun.SetActive(false);
            shotGun.SetActive(false);
            pistolGun.SetActive(true);
        }
    }
}
