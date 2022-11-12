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

        float speedTest = 5f;

        // Vector2 direction movement by player
        Vector2 direction = new Vector2(horizontalMovement, verticalMovement);

        // trigger Move
        _ship.movementEvent.CallMoveShip(direction, speedTest);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Столкнулся");

        if (collision.gameObject==GameManager.Instance.GetPlayerShip())
        {
            Debug.Log("Столкнулся с кораблем игрока");
            GameManager.Instance.GetPlayerShip().GetComponent<Ship>().changeHealthEvent.CallChangeHealthEvent(1);
            _ship.changeHealthEvent.CallChangeHealthEvent(2);
        }

        _ship.gameObject.SetActive(false);
    }


}
