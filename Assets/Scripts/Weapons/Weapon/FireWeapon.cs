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
    /// Выстрел из оружия
    /// </summary>
    private void FireWeaponEvent_OnFireWeapon(FireWeaponEvent fireWeaponEvent, FireWeaponEventArgs fireWeaponEventArgs)
    {
        FireWeaponShip(fireWeaponEventArgs.weaponDetails, fireWeaponEventArgs.direction);
    }

    /// <summary>
    /// Создание префаба и выстрел из оружия
    /// </summary>
    private void FireWeaponShip(WeaponDetailsSO weaponDetails, Vector2 direction)
    {

        #region Validation shoot

        // Оружие не готово к выстрелу
        if (!_ship.weapon.ReadyShoot) return;

        // нет снарядов
        if (_ship.weapon.CurrentAmountAmmo <= 0 && !weaponDetails.isInfinityClip) return;

        #endregion

        GameObject ammoPrefab = weaponDetails.ammoDetailsThisWeapon.ammoPrefab;

        GameObject go = PoolManager.Instance.GetFromThePool(ammoPrefab);

        if (go != null)
        {
            go.SetActive(true); // показываем снаряд
            go.transform.position = transform.position; // Устанавливаем его положение
            float speed = randomAmmoSpeedInThisWeapon(weaponDetails.ammoDetailsThisWeapon.minAmmoSpeed, weaponDetails.ammoDetailsThisWeapon.maxAmmoSpeed); // скорость
            Ammo ammo = go.GetComponent<Ammo>(); // получаем компонтент снаряда
            Vector3 direction3 = new Vector3(direction.x, 0f, 0f); // задаем вектор выстрела
            ammo.InitAmmo(direction3, speed, weaponDetails.ammoDetailsThisWeapon.minAmmoDamage, weaponDetails.ammoDetailsThisWeapon.maxAmmoDagame); // инициализируем его

        }

        // звук выстрела
        WeaponSoundEffect();

        // Устаналиваем оружие на перезарядку
        _ship.weapon.ReadyShoot = false;
        _ship.weapon.Shoot(); // уменьшаем снаряды

    }

    /// <summary>
    /// Случайная скорость снаряда
    /// </summary>
    private float randomAmmoSpeedInThisWeapon(float minSpeed, float maxSpeed)
    {
        return Random.Range(minSpeed, maxSpeed);
    }

    /// <summary>
    /// Эффект выстрела
    /// </summary>
    private void WeaponSoundEffect()
    {
        if (_ship.weapon.weaponShip.soundEffectFire!=null)
        {
            SoundEffectManager.Instance.PlaySoundEffect(_ship.weapon.weaponShip.soundEffectFire);
        }
    }

}
