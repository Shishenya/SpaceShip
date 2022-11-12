using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponUI : MonoBehaviour
{
    [SerializeField ]private TextMeshProUGUI weaponTMP;
    private WeaponDetailsSO _weaponDetails;
    private Weapon _weapon;
    private int _currentAmountAmmo;
    private Ship _playerShip;

    private void OnEnable()
    {
        _playerShip.fireWeaponEvent.OnFireWeapon += FireWeaponEvent_UpdateUI;
    }

    private void OnDisable()
    {
        _playerShip.fireWeaponEvent.OnFireWeapon -= FireWeaponEvent_UpdateUI;
    }

    private void Awake()
    {
        _playerShip = GameManager.Instance.GetPlayerShip().GetComponent<Ship>();
        _weapon = GameManager.Instance.GetPlayerShip().GetComponent<Weapon>();
        _weaponDetails = _weapon.weaponShip;
        InitWeaponUI();
        
    }

    /// <summary>
    /// Отображение снарядов в UI
    /// </summary>
    private void InitWeaponUI()
    {

        string textWeapon;
        _currentAmountAmmo = _weapon.CurrentAmountAmmo;

        if (_weaponDetails.isInfinityClip)
        {
            textWeapon = "Weapon: " + _weaponDetails.nameWeapon + "\nBullets Infinity!";
        } else
        {
            textWeapon = "Weapon: " + _weaponDetails.nameWeapon + "\nBullets: " + _currentAmountAmmo.ToString() + "/ " + _weaponDetails.maxBulletInShip;
        }
        weaponTMP.text = textWeapon;
    }

    private void FireWeaponEvent_UpdateUI(FireWeaponEvent fireWeaponEvent, FireWeaponEventArgs fireWeaponEventArgs)
    {
        InitWeaponUI();
    }

}
