using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChangeHealthEvent : MonoBehaviour
{
    public event Action<ChangeHealthEventArgs> OnChangeHealth;

    public void CallChangeHealthEvent(int damage)
    {
        OnChangeHealth?.Invoke(new ChangeHealthEventArgs { damage = damage });
    }
}

public class ChangeHealthEventArgs: EventArgs
{
    public int damage;
}
