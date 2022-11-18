using UnityEngine;

[RequireComponent(typeof(MovementEvent))]
[DisallowMultipleComponent]
public class NoCollisionObjectMove : MonoBehaviour
{
    private MovementEvent _movementEvent;
    private float _testSpeedMin = 0.3f;
    private float _testSpeedMax = 3f;
    private float _leftBorderX = -17.5f;

    private void Awake()
    {
        _movementEvent = GetComponent<MovementEvent>();
    }

    private void Update()
    {
        if (CheckLeftBorder())
        {
            gameObject.SetActive(false);
        }
        else
        {
            NoCollisionMoveUpdate();
        }
    }

    /// <summary>
    /// �������� ���������
    /// </summary>
    private void NoCollisionMoveUpdate()
    {
        // ������ ��������
        Vector2 direction = new Vector2(-1.5f, 0f);

        // ������� ��������
        float _testSpeed = Random.Range(_testSpeedMin, _testSpeedMax);
        _movementEvent.CallMoveEvent(direction, _testSpeed);
    }

    /// <summary>
    /// ���������, ����� �� ������ �� ����� ������� �� �
    /// </summary>
    private bool CheckLeftBorder()
    {
        if (transform.position.x <= _leftBorderX) return true;

        return false;
    }
}
