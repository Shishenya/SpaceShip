using System.Collections;
using System.Collections.Generic;
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
        // Получаем компоненты
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _movementEvent = GetComponent<MovementEvent>();
    }

    private void OnEnable()
    {
        // подписываемся на события движения
        _movementEvent.OnMoveShip += MovementEvent_OnMoveShip;
    }

    private void OnDisable()
    {
        // отписка
        _movementEvent.OnMoveShip += MovementEvent_OnMoveShip;
    }

    /// <summary>
    /// Движение корабля
    /// </summary>
    private void MovementEvent_OnMoveShip(MovementEvent movementEvent, MovementArgs movementArgs)
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
