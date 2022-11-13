using System.Collections;
using System.Collections.Generic;
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
        if (_enemyShip.weapon.ReadyShoot)
        {
            Debug.Log("� ���� " + _enemyShip.gameObject.name + " �������!");
            _enemyShip.fireWeaponEvent.CallOnFireWeaponEvent(_enemyShip.weaponDetails, new Vector2(-1f,0f));
        }
    }

}
