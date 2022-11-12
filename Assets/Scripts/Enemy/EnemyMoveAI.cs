using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ship))]
[DisallowMultipleComponent]
public class EnemyMoveAI : MonoBehaviour
{
    private Ship _ship;


    private void Awake()
    {
        _ship = GetComponent<Ship>();
    }

    private void Update()
    {
        MovementAI(); // Move enemy ship
    }

    /// <summary>
    /// Метод движения врага
    /// </summary>
    private void MovementAI()
    {
        float horizontalMovement = -1f;
        float verticalMovement = 0f;

        // Vector2 direction movement by player
        Vector2 direction = new Vector2(horizontalMovement, verticalMovement);

        // trigger Move
        _ship.movementEvent.CallMoveShip(direction, _ship._currentShipDetails.speedShip);
    }


}
