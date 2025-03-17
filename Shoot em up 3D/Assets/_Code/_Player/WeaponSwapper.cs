using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSwapper : MonoBehaviour
{
    [SerializeField] private InputActionsHolder inputActionsHolder;

    private GameInputActions _inputActions;

    [SerializeField] private GameObject Gun1;
    [SerializeField] private GameObject Gun2;
    [SerializeField] private GameObject Gun3;

    private WeaponController weaponController1;
    private WeaponController weaponController2;
    private WeaponController weaponController3;

    public static int currentGun;

    private void OnDestroy()
    {
        _inputActions.Player.SwapGun1.performed -= SwapGun1;
        _inputActions.Player.SwapGun2.performed -= SwapGun2;
        _inputActions.Player.SwapGun3.performed -= SwapGun3;
    }

    void Start()
    {
        Prepare();

        weaponController1 = Gun1.GetComponent<WeaponController>();
        weaponController2 = Gun2.GetComponent<WeaponController>();
        weaponController3 = Gun3.GetComponent<WeaponController>();
    }

    private void Prepare()
    {
        _inputActions = inputActionsHolder._GameInputActions;
        _inputActions.Player.SwapGun1.performed += SwapGun1;
        _inputActions.Player.SwapGun2.performed += SwapGun2;
        _inputActions.Player.SwapGun3.performed += SwapGun3;
    }

    private void SwapGun1(InputAction.CallbackContext ctx)
    {
        Gun1.SetActive(true);
        Gun2.SetActive(false);
        Gun3.SetActive(false);
        currentGun = 1;

        StopAllReloads();
        weaponController1.gameObject.SetActive(true); 
    }

    private void SwapGun2(InputAction.CallbackContext ctx)
    {
        Gun1.SetActive(false);
        Gun2.SetActive(true);
        Gun3.SetActive(false);
        currentGun = 2;

        StopAllReloads();
        weaponController2.gameObject.SetActive(true);
    }
    private void SwapGun3(InputAction.CallbackContext ctx)
    {
        Gun1.SetActive(false);
        Gun2.SetActive(false);
        Gun3?.SetActive(true);
        currentGun = 3;

        StopAllReloads();
        weaponController3.gameObject.SetActive(true); 
    }

    private void StopAllReloads()
    {
        if (weaponController1 != null && weaponController1.gameObject.activeInHierarchy)
        {
            weaponController1.StopReloading(); 
        }
        if (weaponController2 != null && weaponController2.gameObject.activeInHierarchy)
        {
            weaponController2.StopReloading(); 
        }
        if (weaponController3 != null && weaponController3.gameObject.activeInHierarchy)
        {
            weaponController3.StopReloading(); 
        }
    }
}
