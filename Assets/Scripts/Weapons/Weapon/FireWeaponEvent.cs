using UnityEngine;
using System;

[DisallowMultipleComponent]
public class FireWeaponEvent : MonoBehaviour
{
    // Ивент выстрела с оружия
    public event Action<FireWeaponEventArgs> OnFireWeapon;

    // Прозвон
    public void CallOnFireWeaponEvent(WeaponDetailsSO weaponDetails, Vector2 direction)
    {
        OnFireWeapon?.Invoke(new FireWeaponEventArgs() { weaponDetails  = weaponDetails , direction = direction });
    }

}

public class FireWeaponEventArgs: EventArgs
{
    public WeaponDetailsSO weaponDetails;
    public Vector2 direction;
}