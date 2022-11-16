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
    private float _offset� = 1f;

    private float _maxBorderY = 6.5f;
    private float _minBorderY = -6.5f;
    private float _offsetBorderY = 1f;


    private void Awake()
    {
        _ship = GetComponent<Ship>();
        _enemyDetails = _ship.GetComponent<Enemy>().EnemyDetails;
    }

    private void Update()
    {
        _playerPosition = GameManager.Instance.GetPlayerShip().transform.position;

        // ���� ������� ���������� ����� ������� ������, �� �������� ��� �������� � ������� ���� � ������� ���������
        if (transform.position.x - _playerPosition.x < _offset�)
        {
            _horizontalMovement = -2f;
            _verticalMovement = 0f;

        }
        // ���� ���� ����������� ����������� � ���� ������ �� Y
        else if (transform.position.y + _offsetBorderY > _maxBorderY)
        {
            _horizontalMovement = -0.5f;
            _verticalMovement = -1f;

        } else if (transform.position.y - _offsetBorderY < _minBorderY)
        {
            _horizontalMovement = -0.5f;
            _verticalMovement = 1f;
        } 
        else 
        {
            GetVectorMovementByType(); // �������� ������ �������� ����� � ����������� �� ��� ���� ��������
        }


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

                // �������� �� ��� �� �����, ��� � �����
                if (Mathf.Abs(_playerPosition.y - transform.position.y) <= _offsetY)
                {
                    _horizontalMovement = -1f;
                    _verticalMovement = 0f;
                }
                else if (_playerPosition.y >= transform.position.y)
                {
                    _horizontalMovement = 0f;
                    _verticalMovement = 1f;
                }
                else
                {
                    _horizontalMovement = -0.25f;
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
        _ship.movementEvent.CallMoveEvent(direction, _ship._currentShipDetails.speedShip);
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
