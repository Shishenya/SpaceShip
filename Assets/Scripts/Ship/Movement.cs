using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(MovementEvent))]
[DisallowMultipleComponent]
public class Movement : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private MovementEvent _movementEvent;

    private void Awake()
    {
        // �������� ����������
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _movementEvent = GetComponent<MovementEvent>();
    }

    private void OnEnable()
    {
        // ������������� �� ������� ��������
        _movementEvent.OnMove += MovementEvent_OnMove;
    }

    private void OnDisable()
    {
        // �������
        _movementEvent.OnMove += MovementEvent_OnMove;
    }

    /// <summary>
    /// ��������
    /// </summary>
    private void MovementEvent_OnMove(MovementArgs movementArgs)
    {
        Move(movementArgs.moveDirection, movementArgs.speed); 
    }

    /// <summary>
    /// 
    /// </summary>
    private void Move(Vector2 direction, float speed)
    {
        _rigidbody2D.velocity = direction * speed;
    }

}
