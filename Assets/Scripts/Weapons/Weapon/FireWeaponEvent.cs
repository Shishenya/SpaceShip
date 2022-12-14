using UnityEngine;
using System;

[DisallowMultipleComponent]
public class FireWeaponEvent : MonoBehaviour
{
    // ????? ???????? ? ??????
    public event Action<FireWeaponEventArgs> OnFireWeapon;

    // ???????
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