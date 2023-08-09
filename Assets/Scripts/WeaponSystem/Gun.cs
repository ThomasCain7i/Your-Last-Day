using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GunData gunData;
    [SerializeField] private Transform cam;
    [SerializeField] private int damage = 10;

    private UI_Manager _uiManager;
    private EnemyHealth enemyHealth;

    float timeSinceLastShot;

    private void Start()
    {
        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;

        _uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
    }

    /*If weapon swithcd cancel reload*/
    private void OnDisable() => gunData.reloading = false;

    public void StartReload()
    {
        if (!gunData.reloading && this.gameObject.activeSelf)
            StartCoroutine(Reload());
    }

    private IEnumerator Reload()
    {
        //Set reloading to true (cant shoot)
        gunData.reloading = true;

        //Wait for the set reload time in gunData class
        yield return new WaitForSeconds(gunData.reloadTime);

        //Set currentAmmo to magSize and update AmmoText
        gunData.currentAmmo = gunData.magSize;
        _uiManager.UpdateAmmo(gunData.currentAmmo);

        //Set reloading to false (can shoot)
        gunData.reloading = false;
    }

    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);

    private void Shoot()
    {
        //Current ammo > 0
        if (gunData.currentAmmo > 0)
        {
            //CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);
            if (CanShoot())
            {
                // Cast ray from camera forwards as far as the current weapons max distance.
                if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hitInfo, gunData.maxDistance))
                {
                    Debug.DrawRay(transform.position, cam.forward, Color.green);
                    //Deal damage using the gunData set damage for that weapon
                    if (hitInfo.collider != null)
                    {
                        Debug.Log("Shot Enemie");
                        gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
                    }
                }
                //Update cuurent ammo on gunshot
                gunData.currentAmmo--;
                _uiManager.UpdateAmmo(gunData.currentAmmo);
                timeSinceLastShot = 0;
                OnGunShot();
            }
        }
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        Debug.DrawRay(cam.position, cam.forward * gunData.maxDistance);
    }

    private void OnGunShot() { }
}
