using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="AmmoDetails_", menuName ="Scriptable Objects/Weapons/Ammo Details")]
public class AmmoDetailsSO : ScriptableObject
{
    #region Header
    [Space(10)]
    [Header("Basic ammo Parameters")]
    #endregion
    [Tooltip("Name ammo")]
    public string ammoName;
    [Tooltip("Prefab ammo")]
    public GameObject ammoPrefab;
    [Tooltip("Sprite ammo")]
    public Sprite ammoSprite;


    #region Header
    [Space(10)]
    [Header("Damage Parameters")]
    #endregion
    [Tooltip("Minimal damage by ammo")]
    public int minAmmoDamage;
    [Tooltip("Maximal damage by ammo")]
    public int maxAmmoDagame;
    [Tooltip("Minimal speed ammo")]
    public int minAmmoSpeed;
    [Tooltip("Maximal speed ammo")]
    public int maxAmmoSpeed;
}
