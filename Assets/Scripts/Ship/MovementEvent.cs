using UnityEngine;
using System;

[DisallowMultipleComponent]
public class MovementEvent : MonoBehaviour
{
    // ����� ������������ �������
    public event Action<MovementArgs> OnMove;

    // ������� ������
    public void CallMoveEvent(Vector2 moveDirection, float speed)
    {
        OnMove.Invoke(new MovementArgs() { moveDirection  = moveDirection , speed  = speed });
    }

}

public class MovementArgs: EventArgs
{
    public Vector2 moveDirection;
    public float speed;
}
