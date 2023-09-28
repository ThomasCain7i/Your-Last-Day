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
    bool shooting, readyToShoot;
    public bool reloading;

    //Reference
    public Camera camera;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy, whatIsWall;

    //Graphics
    public GameObject muzzleFlash, bulletHoleGraphic;
    public TextMeshProUGUI ammoText, gunText;

    //Refs
    [SerializeField] private AudioSource audioSource;
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
            if (!reloading)
            {
                ammoText.SetText(bulletsLeft + " / " + magazineSize);
                gunText.SetText(weaponName);
            }
            else
            {
                ammoText.SetText("Reloading");
            }

        }
    }

    private void MyInput()
    {
        if (!pauseMenu.GameIsPaused)
        {
            if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
            else shooting = Input.GetKeyDown(KeyCode.Mouse0);

            if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();
            if (bulletsLeft < 1 && !reloading && shooting) Reload();

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
            audioSource.PlayOneShot(audioSource.clip, audioSource.volume);
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

            if (Physics.Raycast(camera.transform.position, direction, out rayHit, range, whatIsWall))
            {
                Debug.Log("Raycast hit: " + rayHit.collider.name); // Debug log message

                Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));

                Debug.DrawLine(camera.transform.position, rayHit.point, Color.red, 1.0f);
            }

            //Graphics
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