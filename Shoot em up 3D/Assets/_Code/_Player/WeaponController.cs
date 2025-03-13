using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    private void OnDestroy()
    {
        _inputActions.Player.Shoot.performed -= ShootGun;
        _inputActions.Player.Reload.performed -= ReloadGun;
    }

    private void Start()
    {
        _weaponInfo.currentAmmo = _weaponInfo.magSize;
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
        if (_weaponInfo.currentAmmo > 0 && gameObject.activeInHierarchy)
        {
            Debug.Log("Shooting");
            GameObject bullet = GetBullet();
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = firePoint.rotation;
            _weaponInfo.currentAmmo--;
            timeSinceLastShoot = 0;
            missingBullet.Enqueue(bullet);
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
        if (!_weaponInfo.reloading && gameObject.activeInHierarchy)
        {
            StartCoroutine(Reload());
        }
    }

    public IEnumerator Reload()
    {
        _weaponInfo.reloading = true;
        yield return new WaitForSeconds(_weaponInfo.reloadTime);
        var missingBullets = _weaponInfo.magSize-_weaponInfo.currentAmmo;
        Debug.Log(missingBullets);
        for(int i = 0; i < missingBullets; i++)
        {
            //GameObject bullet = Instantiate(bulletPrefab);
            //bullet.SetActive(false);
            //bulletPool.Enqueue(bulletPrefab);
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
        Debug.Log(bulletPool.Count);
        timeSinceLastShoot += Time.deltaTime;
        Debug.DrawRay(transform.position, transform.forward);
        if (_weaponInfo.reloading == false)
        {
            StopCoroutine(Reload());
        }
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
