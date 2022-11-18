using UnityEngine;

public class FireWeaponAI : MonoBehaviour
{
    private Ship _enemyShip;

    private void Awake()
    {
        _enemyShip = GetComponent<Ship>();
    }

    private void Update()
    {
        Fire();
    }

    private void Fire()
    {
        if (_enemyShip.weapon.ReadyShoot && transform.position.x>GameManager.Instance.GetPlayerShip().transform.position.x)
        {
            // Debug.Log("Я враг " + _enemyShip.gameObject.name + " Стреляю!");
            _enemyShip.fireWeaponEvent.CallOnFireWeaponEvent(_enemyShip.weaponDetails, new Vector2(-1f,0f));
        }
    }

}
