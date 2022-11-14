using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private Ship _enemyShip;

    private void OnEnable()
    {
        _enemyShip.deathEvent.OnDeath += DeathEvent_OnDeathEnemy;
    }

    private void OnDisable()
    {
        _enemyShip.deathEvent.OnDeath -= DeathEvent_OnDeathEnemy;
    }

    private void Awake()
    {
        _enemyShip = GetComponent<Ship>();
    }

    private void DeathEvent_OnDeathEnemy(DeathEventArgs deathEventArgs)
    {
        DeathEnemy();
    }

    /// <summary>
    /// ������ �����
    /// </summary>
    private void DeathEnemy()
    {
        Debug.Log("���� �����");

        // ������������� ����� ��������� ��������
        _enemyShip.health.GetStartHealth();

        // ����������� ���������� ����� ��� ������

        // �������� �����
        EnemySpawner.Instance.DecrementEnemyInScene();

        // ������������
        _enemyShip.gameObject.SetActive(false);
    }

}
