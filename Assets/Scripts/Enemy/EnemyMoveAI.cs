using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ship))]
[DisallowMultipleComponent]
public class EnemyMoveAI : MonoBehaviour
{
    private Ship _ship;
    private EnemyDetailsSO _enemyDetails;

    private float _horizontalMovement;
    private float _verticalMovement;

    private Coroutine _coroutineVectorY;
    private Vector2 _playerPosition;
    private float _offsetY = 0.3f;


    private void Awake()
    {
        _ship = GetComponent<Ship>();
        _enemyDetails = _ship.GetComponent<Enemy>().EnemyDetails;
    }

    private void Update()
    {
        GetVectorMovementByType(); // получаем вектор движения врага

        MovementAI(); // Двигаем его
    }

    private void GetVectorMovementByType()
    {
        switch (_enemyDetails.enemyMoveType)
        {
            // Движение по линии влево
            case EnemyMoveType.HorizontalLine:
                _horizontalMovement = -1f;
                _verticalMovement = 0f;
                break;

            // Случайное движение влево
            case EnemyMoveType.RandomLeft:
                _horizontalMovement = -0.7f;
                if (_coroutineVectorY == null)
                {
                    _coroutineVectorY = StartCoroutine(SetRandomVericalMoveRoutine());
                }
                break;

            // Противник стремится врезаться в игрока
            case EnemyMoveType.Kamikaze:
                _playerPosition = GameManager.Instance.GetPlayerShip().transform.position;
                _horizontalMovement = -1f;
                if (_playerPosition.y <= transform.position.y)
                {
                    _verticalMovement = -0.3f;
                }
                else
                {
                    _verticalMovement = 0.3f;
                }
                break;

            // Противник стремится занять туже линию, что и игрок
            case EnemyMoveType.SearchPlayerY:
                _playerPosition = GameManager.Instance.GetPlayerShip().transform.position;

                // Примрено на той же линии, что и игрок
                if (Mathf.Abs(_playerPosition.y - transform.position.y) <= _offsetY)
                {
                    _horizontalMovement = -1f;
                    _verticalMovement = 0f;
                }
                else if (_playerPosition.y >= transform.position.y)
                {
                    _horizontalMovement = 0.1f;
                    _verticalMovement = 1f;
                }
                else
                {
                    _horizontalMovement = 0.1f;
                    _verticalMovement = -1f;
                }

                break;

            // Дефолтное движение по прямой влево
            default:
                _horizontalMovement = -1f;
                _verticalMovement = 0f;
                break;
        }

    }

    /// <summary>
    /// Метод движения врага
    /// </summary>
    private void MovementAI()
    {

        // Вектор движение корабля противника
        Vector2 direction = new Vector2(_horizontalMovement, _verticalMovement);

        // Триггер движения
        _ship.movementEvent.CallMoveShip(direction, _ship._currentShipDetails.speedShip);
    }

    /// <summary>
    /// Случайное значение вектора Y для корабля противника со сменой в 2 секунды
    /// </summary>
    /// <returns></returns>
    private IEnumerator SetRandomVericalMoveRoutine()
    {
        _verticalMovement = Random.Range(-1f, 1f);
        yield return new WaitForSeconds(2f);
        _coroutineVectorY = null;
    }


}
