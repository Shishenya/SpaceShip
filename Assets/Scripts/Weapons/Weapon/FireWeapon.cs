using UnityEngine;

[RequireComponent(typeof(FireWeaponEvent))]
[DisallowMultipleComponent]
public class FireWeapon : MonoBehaviour
{
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
    private void FireWeaponEvent_OnFireWeapon(FireWeaponEventArgs fireWeaponEventArgs)
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

        }

        // ���� ��������
        WeaponSoundEffect();

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

    /// <summary>
    /// ������ ��������
    /// </summary>
    private void WeaponSoundEffect()
    {
        if (_ship.weapon.weaponShip.soundEffectFire!=null)
        {
            SoundEffectManager.Instance.PlaySoundEffect(_ship.weapon.weaponShip.soundEffectFire);
        }
    }

}
