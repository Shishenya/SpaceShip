using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Ship _ship;
    [HideInInspector] public WeaponDetailsSO weaponShip;

    private float _reloadTimer = 0; // “аймер перезар€дки
    private bool _isReadyShoot = false; // √отовность стрел€ть

    private int _currentAmountAmmo = 0;
    private int _maxAmountAmmo = 0;

    public bool ReadyShoot
    {
        get { return _isReadyShoot;  }
        set { _isReadyShoot = value;  }

    }

    public int CurrentAmountAmmo
    {
        get { return _currentAmountAmmo; }
    }


    private void Awake()
    {
        _ship = GetComponent<Ship>();
        InitWeapon(_ship.weaponDetails);
        _reloadTimer = weaponShip.reloadTimeWeapon;
        _currentAmountAmmo = weaponShip.maxBulletInShip; // устанавлиаем текущее количество снар€дом как максимальное
        _maxAmountAmmo = weaponShip.maxBulletInShip;

    }

    /// <summary>
    ///  »нициализаци€ оружи€
    /// </summary>
    private void InitWeapon(WeaponDetailsSO weaponDetailsSO)
    {
        weaponShip = weaponDetailsSO;
    }

    private void Update()
    {
        if (!_isReadyShoot)
            _reloadTimer -= Time.deltaTime;

        if (_reloadTimer<=0)
        {
            _isReadyShoot = true;
            _reloadTimer = weaponShip.reloadTimeWeapon;
        }
    }

    public void Shoot()
    {
        if (weaponShip.isInfinityClip) return; // если боеприпасы бесконечные

        _currentAmountAmmo--;
        if (_currentAmountAmmo < 0) _currentAmountAmmo = 0;
    }

}
