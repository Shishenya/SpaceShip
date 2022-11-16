using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="WeaponDetails_", menuName = "Scriptable Objects/Weapons/Weapon Details")]
public class WeaponDetailsSO : ScriptableObject
{
    #region Basic Parameters
    [Space(10)]
    [Header("Basic Parameters")]
    #endregion Basic Parameters

    [Tooltip("Name by weapon")]
    public string nameWeapon = "Default Name";
    [Tooltip("Ammo use in this weapon")]
    public AmmoDetailsSO ammoDetailsThisWeapon = null;

    #region Parameters weapon 
    [Space(10)]
    [Header("Parameters weapon")]
    #endregion

    [Tooltip("This weapon use Infinity bullet?")]
    public bool isInfinityClip = false;
    [Tooltip("Reload time")]
    public float reloadTimeWeapon = 0.2f;
    [Tooltip("Current bullet in ship space")]
    public int currentBulletInShip = 0;
    [Tooltip("max bullet in ship space")]
    public int maxBulletInShip = 100;

    #region Sound weapon 
    [Space(10)]
    [Header("Sound weapon")]
    #endregion
    public SoundEffectSO soundEffectFire;

}
