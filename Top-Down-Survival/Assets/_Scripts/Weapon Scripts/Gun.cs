using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EZCameraShake;

public class Gun : MonoBehaviour
{
    [SerializeField] BulletPooler _objectPooler;
    [SerializeField] GunSO _gun;
    [SerializeField] SpriteRenderer _gunSprite;

    private Rigidbody2D playerRB;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private GameObject _bullet;

    private int _bulletsLeft, _bulletsShot;
    private bool _shooting, _readyToShoot, _reloading, _needsToReload, _canShoot;

    private Vector2 _mousePos, _direction;
    
    [SerializeField] private TextMeshProUGUI _ammoText;
    [SerializeField] private Transform _ammoPos;
    [SerializeField] private Transform _pos1;
    [SerializeField] private Transform _pos2;

    private Camera _cam;

    private void Start()
    {
        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();

        _bulletsLeft = _gun.magazineSize;
        _readyToShoot = true;
        _cam = Camera.main;
    }

    void Update()
    {
        _canShoot = _shooting && _readyToShoot && !_reloading && !_needsToReload && !Pause.isPaused && !PlayerMovement.IsDashing;
        _needsToReload = _bulletsLeft == 0;

        if (_gun.allowButtonHold) _shooting = Input.GetKey(KeyCode.Mouse0);
        else _shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && _bulletsLeft < _gun.magazineSize && !_reloading) StartCoroutine(Reload());

        if (_needsToReload && !_reloading) StartCoroutine(Reload());

        if (_canShoot)
        {
            _bulletsShot = _gun.bulletsPerTap;
            Shoot();
        }

        if (_reloading)
        {
            _ammoText.SetText("[Rerolling...]");
        }
        else
        {
            _ammoText.SetText("" + _bulletsLeft + " | " + _gun.magazineSize + "");
        }

        _mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
        _direction = _mousePos - (Vector2)transform.position;
        transform.right = _direction;

        if (_mousePos.x <= transform.position.x)
        {
            _gunSprite.flipY = true;
            _ammoPos.position = _pos1.position;
        }
        else
        {
            _gunSprite.flipY = false;
            _ammoPos.position = _pos2.position;
        }

        if (PlayerMovement.IsDashing && !_reloading)
        {
            _gun.magazineSize = Random.Range(1, 7);
            StartCoroutine(Reload());
        }
    }

    void Shoot()
    {
        _readyToShoot = false;

        float x = Random.Range(-_gun.spread, _gun.spread);
        float y = Random.Range(-_gun.spread, _gun.spread);
        Vector2 direction = new Vector3(x,y) + _shootPoint.up;

        GameObject t_bullet = _objectPooler.GetPooledObject();
        t_bullet.transform.position = _shootPoint.position;
        t_bullet.transform.rotation = _shootPoint.rotation;
        t_bullet.GetComponent<PlayerBullet>().LifeTime = _gun.bulletLifeTime;
        t_bullet.SetActive(true);

        t_bullet.GetComponent<PlayerBullet>().Damage = _gun.PlayerDamage;

        Rigidbody2D bulletRb = t_bullet.GetComponent<Rigidbody2D>();
        bulletRb.AddForce(direction * _gun.bulletSpeed, ForceMode2D.Impulse);
        

        if (_gun.canShake)
        {
            CameraShaker.Instance.ShakeOnce(_gun.magnitude, _gun.roughness, _gun.fadeIn, _gun.fadeOut);
        }

        _bulletsLeft--;
        _bulletsShot--;

        if (t_bullet = null) return;

        Invoke("ResetShot", _gun.timeBetweenShooting);

        if (_bulletsShot > 0 && _bulletsLeft > 0)
            Invoke("Shoot", _gun.timeBetweenShots);
    }

    private void ResetShot()
    {
        _readyToShoot = true;
    }

    IEnumerator Reload()
    {
        _reloading = true;

        yield return Waiter.GetWait(_gun.reloadTime);

        _bulletsLeft = _gun.magazineSize;
        _reloading = false;
    }
}
