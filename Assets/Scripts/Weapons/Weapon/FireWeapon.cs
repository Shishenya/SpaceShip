using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FireWeaponEvent))]
[DisallowMultipleComponent]
public class FireWeapon : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private FireWeaponEvent _fireWeaponEvent;
    private Ship _ship;

    private void Awake()
    {
        _fireWeaponEvent = GetComponent<FireWeaponEvent>();
        _ship = GetComponent<Ship>();
    }

    private void OnEnable()
    {
        _fireWeaponEvent.OnFireWeapon += FireWeaponEvent_OnFireWeapon;
    }

    private void OnDisable()
    {
        _fireWeaponEvent.OnFireWeapon -= FireWeaponEvent_OnFireWeapon;
    }

    /// <summary>
    /// ������� �� ������
    /// </summary>
    private void FireWeaponEvent_OnFireWeapon(FireWeaponEvent fireWeaponEvent, FireWeaponEventArgs fireWeaponEventArgs)
    {
        FireWeaponShip(fireWeaponEventArgs.weaponDetails, fireWeaponEventArgs.direction);
    }

    /// <summary>
    /// �������� ������� � ������� �� ������
    /// </summary>
    private void FireWeaponShip(WeaponDetailsSO weaponDetails, Vector2 direction)
    {

        #region Validation shoot

        // ������ �� ������ � ��������
        if (!_ship.weapon.ReadyShoot) return;

        // ��� ��������
        if (_ship.weapon.CurrentAmountAmmo <= 0 && !weaponDetails.isInfinityClip) return;

        #endregion

        GameObject ammoPrefab = weaponDetails.ammoDetailsThisWeapon.ammoPrefab;

        GameObject go = PoolManager.Instance.GetFromThePool(ammoPrefab);

        if (go != null)
        {
            go.SetActive(true); // ���������� ������
            go.transform.position = transform.position; // ������������� ��� ���������
            float speed = randomAmmoSpeedInThisWeapon(weaponDetails.ammoDetailsThisWeapon.minAmmoSpeed, weaponDetails.ammoDetailsThisWeapon.maxAmmoSpeed); // ��������
            Ammo ammo = go.GetComponent<Ammo>(); // �������� ���������� �������
            Vector3 direction3 = new Vector3(direction.x, 0f, 0f); // ������ ������ ��������
            ammo.InitAmmo(direction3, speed, weaponDetails.ammoDetailsThisWeapon.minAmmoDamage, weaponDetails.ammoDetailsThisWeapon.maxAmmoDagame); // �������������� ���

            //go.GetComponent<Rigidbody2D>().velocity = new Vector2(1f, 0f) * speed; // �������� ���            
        }

        // ������������ ������ �� �����������
        _ship.weapon.ReadyShoot = false;
        _ship.weapon.Shoot(); // ��������� �������

    }

    /// <summary>
    /// ��������� �������� �������
    /// </summary>
    private float randomAmmoSpeedInThisWeapon(float minSpeed, float maxSpeed)
    {
        return Random.Range(minSpeed, maxSpeed);
    }

}
