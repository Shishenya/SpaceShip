using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementEvent))]
[DisallowMultipleComponent]
public class NoCollisionObjectMove : MonoBehaviour
{
    private MovementEvent _movementEvent;
    private float _testSpeedMin = 0.3f;
    private float _testSpeedMax = 3f;

    private void Awake()
    {
        _movementEvent = GetComponent<MovementEvent>();
    }

    private void Update()
    {
        NoCollisionMoveUpdate();
    }

    /// <summary>
    /// �������� ���������
    /// </summary>
    private void NoCollisionMoveUpdate()
    {
        // ������ �������� ������� ����������
        Vector2 direction = new Vector2(-1.5f, 0f);

        // ������� ��������
        float _testSpeed = Random.Range(_testSpeedMin, _testSpeedMax);
        _movementEvent.CallMoveEvent(direction, _testSpeed);
    }
}
