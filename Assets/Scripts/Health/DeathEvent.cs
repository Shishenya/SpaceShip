using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeathEvent : MonoBehaviour
{
    public event Action<DeathEventArgs> OnDeath;

    public void CallOnDeathEvent()
    {
        OnDeath?.Invoke(new DeathEventArgs() { } );
    }

}

public class DeathEventArgs: EventArgs
{

}