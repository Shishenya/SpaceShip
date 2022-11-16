using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DisallowMultipleComponent]
public class MovementEvent : MonoBehaviour
{
    // ����� ������������ �������
    public event Action<MovementEvent, MovementArgs> OnMove;

    // ������� ������
    public void CallMoveEvent(Vector2 moveDirection, float speed)
    {
        OnMove.Invoke(this, new MovementArgs() { moveDirection  = moveDirection , speed  = speed });
    }

}

public class MovementArgs: EventArgs
{
    public Vector2 moveDirection;
    public float speed;
}
