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
        GetVectorMovementByType(); // �������� ������ �������� �����

        MovementAI(); // ������� ���
    }

    private void GetVectorMovementByType()
    {
        switch (_enemyDetails.enemyMoveType)
        {
            // �������� �� ����� �����
            case EnemyMoveType.HorizontalLine:
                _horizontalMovement = -1f;
                _verticalMovement = 0f;
                break;

            // ��������� �������� �����
            case EnemyMoveType.RandomLeft:
                _horizontalMovement = -0.7f;
                if (_coroutineVectorY == null)
                {
                    _coroutineVectorY = StartCoroutine(SetRandomVericalMoveRoutine());
                }
                break;

            // ��������� ��������� ��������� � ������
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

            // ��������� ��������� ������ ���� �����, ��� � �����
            case EnemyMoveType.SearchPlayerY:
                _playerPosition = GameManager.Instance.GetPlayerShip().transform.position;

                // �������� �� ��� �� �����, ��� � �����
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

            // ��������� �������� �� ������ �����
            default:
                _horizontalMovement = -1f;
                _verticalMovement = 0f;
                break;
        }

    }

    /// <summary>
    /// ����� �������� �����
    /// </summary>
    private void MovementAI()
    {

        // ������ �������� ������� ����������
        Vector2 direction = new Vector2(_horizontalMovement, _verticalMovement);

        // ������� ��������
        _ship.movementEvent.CallMoveShip(direction, _ship._currentShipDetails.speedShip);
    }

    /// <summary>
    /// ��������� �������� ������� Y ��� ������� ���������� �� ������ � 2 �������
    /// </summary>
    /// <returns></returns>
    private IEnumerator SetRandomVericalMoveRoutine()
    {
        _verticalMovement = Random.Range(-1f, 1f);
        yield return new WaitForSeconds(2f);
        _coroutineVectorY = null;
    }


}
