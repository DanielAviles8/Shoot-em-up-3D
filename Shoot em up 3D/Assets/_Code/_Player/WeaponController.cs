using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private InputActionsHolder inputActionsHolder;
    private GameInputActions _inputActions;

    [SerializeField] private WeaponInfo _weaponInfo;
    [SerializeField] private float timeSinceLastShoot;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    //[SerializeField] private BulletPool Instance;

    private Queue<GameObject> bulletPool = new Queue<GameObject>();
    private Queue<GameObject> missingBullet = new Queue<GameObject>();

    [SerializeField] private Transform Shotgun1;
    [SerializeField] private Transform Shotgun2;
    [SerializeField] private Transform Shotgun3;
    [SerializeField] private Transform Shotgun4;
    [SerializeField] private Transform Shotgun5;

    [SerializeField] private TextMeshProUGUI bulletRemaining;

    public Slider reloadSlider;
    private void OnDestroy()
    {
        _inputActions.Player.Shoot.performed -= ShootGun;
        _inputActions.Player.Reload.performed -= ReloadGun;
    }

    private void Start()
    {
        bulletRemaining.text = _weaponInfo.currentAmmo.ToString();
        _weaponInfo.reloading = false;
        _weaponInfo.currentAmmo = _weaponInfo.magSize;
        reloadSlider.gameObject.SetActive(false);
        Prepare();
        /*for(int i = 0; i < bulletPool.Count; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.GetComponent<BulletBehaviour>()._weaponController = this;
            bullet.SetActive(false);
            bulletPool.Enqueue(bullet);
        }*/
        while(bulletPool.Count < _weaponInfo.magSize)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.GetComponent<BulletBehaviour>()._weaponController = this;
            bullet.SetActive(false);
            bulletPool.Enqueue(bullet);
        }
        while(bulletPool.Count > _weaponInfo.magSize)
        {
            bulletPool.Dequeue();
        }
    }

    private void Prepare()
    {
        _inputActions = inputActionsHolder._GameInputActions;
        _inputActions.Player.Shoot.performed += ShootGun;
        _inputActions.Player.Reload.performed += ReloadGun;
    }

    private bool CanShoot() => !_weaponInfo.reloading && timeSinceLastShoot > 1f / (_weaponInfo.fireRate / 60f);

    private void ShootGun(InputAction.CallbackContext ctx)
    {
        if (CameraBehaviour._inMenu || CameraBehaviour._inCinematic) return;
        if (_weaponInfo.currentAmmo > 0 && gameObject.activeInHierarchy)
        {
            if(WeaponSwapper.currentGun == 3)
            {
                GameObject bullet0 = GetBullet();
                bullet0.transform.position = firePoint.position;
                bullet0.transform.rotation = firePoint.rotation;
                Debug.Log("Bala 0");
                missingBullet.Enqueue(bullet0);

                GameObject bullet1 = GetBullet();
                bullet1.transform.position = Shotgun1.position; 
                bullet1.transform.rotation = Shotgun1.rotation;
                Debug.Log("Bala 1");
                missingBullet.Enqueue(bullet1);

                GameObject bullet2 = GetBullet();
                bullet2.transform.position = Shotgun2.position;
                bullet2.transform.rotation = Shotgun2.rotation;
                Debug.Log("Bala 2");
                missingBullet.Enqueue(bullet2);

                GameObject bullet3 = GetBullet();
                bullet3.transform.position = Shotgun3.position;
                bullet3.transform.rotation = Shotgun3.rotation;
                Debug.Log("Bala 0");
                missingBullet.Enqueue(bullet3);

                GameObject bullet4 = GetBullet();
                bullet4.transform.position = Shotgun4.position;
                bullet4.transform.rotation = Shotgun4.rotation;
                Debug.Log("Bala 1");
                missingBullet.Enqueue(bullet4);

                GameObject bullet5 = GetBullet();
                bullet5.transform.position = Shotgun5.position;
                bullet5.transform.rotation = Shotgun5.rotation;
                Debug.Log("Bala 2");
                missingBullet.Enqueue(bullet5);

                _weaponInfo.currentAmmo = _weaponInfo.currentAmmo - 6;
            }
            else if(WeaponSwapper.currentGun == 1 || WeaponSwapper.currentGun == 2)
            {
                GameObject bullet = GetBullet();
                bullet.transform.position = firePoint.position;
                bullet.transform.rotation = firePoint.rotation;
                _weaponInfo.currentAmmo--;
                missingBullet.Enqueue(bullet);
            }
            timeSinceLastShoot = 0;
        }
    }
    public GameObject GetBullet()
    {
        if(bulletPool.Count > 0)
        {
            GameObject bullet = bulletPool.Dequeue();
            bullet.SetActive(true);
            return bullet;
        }
        else
        {
            return null;
        }
    }
    public void ReturnBulelt(GameObject bullet)
    {
        bulletPool.Enqueue(bullet);
        bullet.SetActive(false);
    }
    public void ReloadGun(InputAction.CallbackContext ctx)
    {
        if (CameraBehaviour._inMenu || CameraBehaviour._inCinematic) return;
        if (!_weaponInfo.reloading && gameObject.activeInHierarchy)
        {
            StartCoroutine(Reload());
            reloadSlider.gameObject.SetActive(true);
        }
    }
    public void ReloadEmptyGun()
    {
        if(_weaponInfo.reloading == false && gameObject.activeInHierarchy && _weaponInfo.currentAmmo == 0)
        {
            StartCoroutine(Reload());
            reloadSlider.gameObject.SetActive(true);
        }
    }
    public IEnumerator Reload()
    {
        _weaponInfo.reloading = true;
        float elapsedTime = 0f;
        float reloadTime = _weaponInfo.reloadTime;

        reloadSlider.value = 0f;

        while (elapsedTime < reloadTime)
        {
            elapsedTime += Time.deltaTime;
            reloadSlider.value = Mathf.Clamp01(elapsedTime / reloadTime); // Normaliza el progreso
            yield return null;
        }

        reloadSlider.value = 1f; // Asegura que el valor final sea 1
        reloadSlider.gameObject.SetActive(false);

        // Manejo de las balas
        var missingBullets = _weaponInfo.magSize - _weaponInfo.currentAmmo;
        for (int i = 0; i < missingBullets; i++)
        {
            var spentBullet = missingBullet.Dequeue();
            ReturnBulelt(spentBullet);
        }
        while (bulletPool.Count > _weaponInfo.magSize)
        {
            bulletPool.Dequeue();
        }

        _weaponInfo.currentAmmo = _weaponInfo.magSize;
        _weaponInfo.reloading = false;
    }

    private void Update()
    {
        bulletRemaining.text = _weaponInfo.currentAmmo.ToString();
        timeSinceLastShoot += Time.deltaTime;
        Debug.DrawRay(transform.position, transform.forward);
        if (_weaponInfo.reloading == false)
        {
            StopCoroutine(Reload());
            reloadSlider.gameObject.SetActive(false);
        }
        ReloadEmptyGun();
    }

    public void StopReloading()
    {
        if (_weaponInfo.reloading)
        {
            StopCoroutine(Reload());
            _weaponInfo.reloading = false;
        }
    }
}
