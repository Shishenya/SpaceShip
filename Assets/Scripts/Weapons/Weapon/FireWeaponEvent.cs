using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DisallowMultipleComponent]
public class FireWeaponEvent : MonoBehaviour
{
    // ����� �������� � ������
    public event Action<FireWeaponEvent, FireWeaponEventArgs> OnFireWeapon;

    // �������
    public void CallOnFireWeaponEvent(WeaponDetailsSO weaponDetails, Vector2 direction)
    {
        OnFireWeapon?.Invoke(this, new FireWeaponEventArgs() { weaponDetails  = weaponDetails , direction = direction });
    }

}

public class FireWeaponEventArgs: EventArgs
{
    public WeaponDetailsSO weaponDetails;
    public Vector2 direction;
}