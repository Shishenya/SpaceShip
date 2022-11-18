using UnityEngine;
using System;

public class DeathEvent : MonoBehaviour
{
    public event Action OnDeath;

    public void CallOnDeathEvent()
    {
        OnDeath?.Invoke();
    }

}
