using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DisallowMultipleComponent]
public class MovementEvent : MonoBehaviour
{
    // ����� ������������ �������
    public event Action<MovementEvent, MovementArgs> OnMoveShip;

    // ������� ������
    public void CallMoveShip(Vector2 moveDirection, float speed)
    {
        OnMoveShip?.Invoke(this, new MovementArgs() { moveDirection  = moveDirection , speed  = speed });
    }

}

public class MovementArgs: EventArgs
{
    public Vector2 moveDirection;
    public float speed;
}
