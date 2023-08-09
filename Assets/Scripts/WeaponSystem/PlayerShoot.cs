using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public static Action shootInput;
    public static Action reloadInput;

    [SerializeField] private KeyCode reloadKey = KeyCode.R;

    private void Update()
    {
        //If the left mouse is held/pressed invoke shootInput
        if (Input.GetMouseButton(0))
            shootInput?.Invoke();
        //If the R key is pressed invoke reloadInput
        if (Input.GetKeyDown(reloadKey))
            reloadInput?.Invoke();
    }
}
