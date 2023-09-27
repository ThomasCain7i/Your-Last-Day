using UnityEngine;
using TMPro;

public class GunSystem : MonoBehaviour
{
    //Gun stats
    public string weaponName;
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    //bools 
    bool shooting, readyToShoot, reloading;

    //Reference
    public Camera camera;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy, whatIsWall;

    //Graphics
    public GameObject muzzleFlash, bulletHoleGraphic;
    public TextMeshProUGUI ammoText, gunText;

    //Refs

    [SerializeField] private PauseMenu pauseMenu;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }
    private void Update()
    {
        if (!pauseMenu.GameIsPaused)
        {
            MyInput();

            //SetText
            ammoText.SetText(bulletsLeft + " / " + magazineSize);
            gunText.SetText(weaponName);
        }
    }
    private void MyInput()
    {
        if (!pauseMenu.GameIsPaused)
        {
            if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
            else shooting = Input.GetKeyDown(KeyCode.Mouse0);

            if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();

            //Shoot
            if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
            {
                bulletsShot = bulletsPerTap;
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        if (!pauseMenu.GameIsPaused)
        {
            readyToShoot = false;

            //Spread
            float x = Random.Range(-spread, spread);
            float y = Random.Range(-spread, spread);

            //Calculate Direction with Spread
            Vector3 direction = camera.transform.forward + new Vector3(x, y, 0);

            if (Physics.Raycast(camera.transform.position, direction, out rayHit, range, whatIsEnemy))
            {
                Debug.Log("Raycast hit: " + rayHit.collider.name); // Debug log message

                if (rayHit.collider.CompareTag("Enemy"))
                    rayHit.collider.GetComponent<EnemyHealth>().TakeDamage(damage);

                Debug.DrawLine(camera.transform.position, rayHit.point, Color.red, 1.0f);
            }

            //Graphics
            Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));
            Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

            bulletsLeft--;
            bulletsShot--;

            Invoke("ResetShot", timeBetweenShooting);

            if (bulletsShot > 0 && bulletsLeft > 0)
                Invoke("Shoot", timeBetweenShots);
        }
    }


    private void ResetShot()
    {
        if (!pauseMenu.GameIsPaused)
        {
            readyToShoot = true;
        }

    }
    private void Reload()
    {
        if (!pauseMenu.GameIsPaused)
        {
            reloading = true;
            Invoke("ReloadFinished", reloadTime);
        }

    }
    private void ReloadFinished()
    {
        if (!pauseMenu.GameIsPaused)
        {
            bulletsLeft = magazineSize;
            reloading = false;
        }

    }
}