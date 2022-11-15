using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyDetailsSO enemyDetails;

    private Ship _enemyShip;

    public EnemyDetailsSO EnemyDetails
    {
        get
        {
            return enemyDetails;
        }
    }

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
        // Debug.Log("���� �����");

        // ������������� ����� ��������� ��������
        _enemyShip.health.GetStartHealth();

        // ����������� ���������� ����� ��� ������
        GameManager.Instance.GameScore = _enemyShip.health.StartHealth;
        GameManager.Instance.playerScoreUI.UpdateScore();

        // �������� �����
        EnemySpawner.Instance.DecrementEnemyInScene();

        // ������������
        _enemyShip.gameObject.SetActive(false);

        // ���������, ���� �� ��� ��������� ������ �� �����, �� ���������� �������
        if (EnemySpawner.Instance.GetRemainsToSpawnEnemy() <= 0 && EnemySpawner.Instance.CurrentEnemyInScene == 0)
        {
            GameManager.Instance.GameNextLevel();
        }
    }

}
