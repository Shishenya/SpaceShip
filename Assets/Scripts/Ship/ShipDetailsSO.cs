using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ShipDetails_", menuName ="Scriptable Objects/Ship/Ship Details")]
public class ShipDetailsSO : ScriptableObject
{
    #region HEADER Basic DETAILS
    [Space(10)]
    [Header("BASIC DETAILS SHIP")]
    #endregion HEADER Basic DETAILS
    [Tooltip("Name ship")]
    public string nameShip;
    [Tooltip("Prefab ship")]
    public GameObject prefabShip;


    #region Header POSITION WEAPON AND EFFECT
    [Space(10)]
    [Header("POSITION WEAPON AND EFFECT")]
    #endregion Header POSITION WEAPON AND EFFECT

    #region Header MOVE
    [Space(10)]
    [Header("MOVE")]
    #endregion
    [Tooltip("Speed ship")]
    public float speedShip;

    #region Header HEALTH
    [Space(10)]
    [Header("HEALTH")]
    #endregion
    public int startHealth = 10;
    public GameObject deathEffect;

}
