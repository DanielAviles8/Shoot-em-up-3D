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

    private void OnDestroy()
    {
        _inputActions.Player.Shoot.performed -= ShootGun;
        _inputActions.Player.Reload.performed -= ReloadGun;
    }

    private void Start()
    {
        Prepare();
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
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            _weaponInfo.currentAmmo--;
            timeSinceLastShoot = 0;
        }
    }

    private void ReloadGun(InputAction.CallbackContext ctx)
    {
        if (!_weaponInfo.reloading && gameObject.activeInHierarchy)
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        _weaponInfo.reloading = true;
        yield return new WaitForSeconds(_weaponInfo.reloadTime);
        _weaponInfo.currentAmmo = _weaponInfo.magSize;
        _weaponInfo.reloading = false;
    }

    private void Update()
    {
        timeSinceLastShoot += Time.deltaTime;
        Debug.DrawRay(transform.position, transform.forward);
        if (_weaponInfo.reloading == false)
        {
            StopAllCoroutines();
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
