using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Ship))]
[DisallowMultipleComponent]
public class PlayerController : MonoBehaviour
{

    private Ship _ship;

    private void Awake()
    {
        _ship = GetComponent<Ship>(); // Получаем компонет корабля        
    }

    private void Update()
    {
        MovementInput(); // Move player ship

        FireInput(); // Fire player ship
    }

    /// <summary>
    /// Movement Player Ship
    /// </summary>
    private void MovementInput()
    {
        // Get movement input
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");

        // Vector2 direction movement by player
        Vector2 direction = new Vector2(horizontalMovement, verticalMovement);

        // trigger Move
        _ship.movementEvent.CallMoveShip(direction, _ship._currentShipDetails.speedShip);

    }

    /// <summary>
    /// Fire Input ship
    /// </summary>
    private void FireInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _ship.fireWeaponEvent.CallOnFireWeaponEvent(_ship.weaponDetails, Vector2.one);
        }
    }

}
