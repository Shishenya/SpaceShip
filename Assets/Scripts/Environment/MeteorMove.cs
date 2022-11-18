using UnityEngine;

[RequireComponent(typeof(MovementEvent))]
[RequireComponent(typeof(Movement))]
[DisallowMultipleComponent]
public class MeteorMove : MonoBehaviour
{
    private MovementEvent _movementEvent;
    private float _speed = 3f;

    private void Awake()
    {
        _movementEvent = GetComponent<MovementEvent>();
    }

    private void Update()
    {
        MeteorMoveUpdate();
    }

    /// <summary>
    /// Движение метеорита
    /// </summary>
    private void MeteorMoveUpdate()
    {
        // Вектор движение метеора
        Vector2 direction = new Vector2(-1f, 0f);

        // Триггер движения
        _movementEvent.CallMoveEvent(direction, _speed);
    }
}
