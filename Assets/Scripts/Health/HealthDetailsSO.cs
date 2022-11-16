using UnityEngine;

[CreateAssetMenu(fileName = "HealthDetails_", menuName = "Scriptable Objects/Health/Health Details")]
public class HealthDetailsSO : ScriptableObject
{
    public int startHealth = 10;
    public SoundEffectSO soundDestroy; 
}
