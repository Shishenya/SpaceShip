using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BonusDetails_", menuName = "Scriptable Objects/Bonus/Bonus Details")]
public class BonusDetailsSO : ScriptableObject
{
    public string nameBonus;
    public int addHealthBonus = 0;
}
