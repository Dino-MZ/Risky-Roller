using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EZCameraShake;

public class Gun : MonoBehaviour
{
    public BulletPooler objectPooler;
    public GunSO gun;

    private Rigidbody2D playerRB;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject Bullet;

    private int bulletsLeft, bulletsShot;
    private bool shooting, readyToShoot, reloading, needsToReload, canShoot;

    private Vector2 mousePos, direction;
    
    [SerializeField] private TextMeshProUGUI ammoText;

    public SpriteRenderer gunSprite;
    private Camera cam;

    private void Start()
    {
        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();

        bulletsLeft = gun.magazineSize;
        readyToShoot = true;
        cam = Camera.main;
    }

    void Update()
    {
        canShoot = shooting && readyToShoot && !reloading && !needsToReload && !Pause.isPaused;
        needsToReload = bulletsLeft == 0;

        if (gun.allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < gun.magazineSize && !reloading) StartCoroutine(Reload());

        if (needsToReload && !reloading) StartCoroutine(Reload());

        if (canShoot)
        {
            bulletsShot = gun.bulletsPerTap;
            Shoot();
        }

        if (reloading)
        {
            ammoText.SetText("[Rerolling...]");
        }
        else
        {
            ammoText.SetText("" + bulletsLeft + " | " + gun.magazineSize + "");
        }

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePos - (Vector2)transform.position;
        transform.right = direction;

        if (mousePos.x <= transform.position.x)
        {
            gunSprite.flipY = true;
        }
        else
        {
            gunSprite.flipY = false;
        }
    }

    void Shoot()
    {
        readyToShoot = false;

        float x = Random.Range(-gun.spread, gun.spread);
        float y = Random.Range(-gun.spread, gun.spread);
        Vector2 direction = new Vector3(x,y) + shootPoint.up;

        GameObject t_bullet = objectPooler.GetPooledObject();
        t_bullet.transform.position = shootPoint.position;
        t_bullet.transform.rotation = shootPoint.rotation;
        t_bullet.GetComponent<PlayerBullet>().LifeTime = gun.bulletLifeTime;
        t_bullet.SetActive(true);

        t_bullet.GetComponent<PlayerBullet>().Damage = gun.PlayerDamage;

        Rigidbody2D bulletRb = t_bullet.GetComponent<Rigidbody2D>();
        bulletRb.AddForce(direction * gun.bulletSpeed, ForceMode2D.Impulse);
        

        if (gun.canShake)
        {
            CameraShaker.Instance.ShakeOnce(gun.magnitude, gun.roughness, gun.fadeIn, gun.fadeOut);
        }

        bulletsLeft--;
        bulletsShot--;

        if (t_bullet = null) return;

        Invoke("ResetShot", gun.timeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", gun.timeBetweenShots);
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    IEnumerator Reload()
    {
        reloading = true;

        yield return Waiter.GetWait(gun.reloadTime);

        bulletsLeft = gun.magazineSize;
        reloading = false;
    }
}
