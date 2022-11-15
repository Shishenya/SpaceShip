using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="LevelDetails_", menuName = "Scriptable Objects/Levels/Level Details")]
public class LevelDetailsSO : ScriptableObject
{
    #region Basic level options
    [Space(10)]
    [Header("Basic level options")]
    #endregion
    [Tooltip("Name level")]
    public string nameLevel;
    [Tooltip("Background level")]
    public Sprite backgroundLevel;


    #region Enemy level options
    [Space(10)]
    [Header("Enemy level options")]
    #endregion
    [Tooltip("Amount enemy level")]
    public int amountEnemyInLevel = 25;
    [Tooltip("Max enemy concurent")]
    public int maxEnemyConcurent = 3;
    [Tooltip("Interval spawn by enemy")]
    public float intervalEnemySpawner = 3f;

    #region Spawn Enemy Details
    [Space(10)]
    [Header("Spawn Enemy Details")]
    #endregion
    [Tooltip("Enemy GO in this level")]
    public List<GameObject> enemyGOList;


}
