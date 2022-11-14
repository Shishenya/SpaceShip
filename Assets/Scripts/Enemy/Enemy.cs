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
    /// Смерть врага
    /// </summary>
    private void DeathEnemy()
    {
        Debug.Log("Враг погиб");

        // Устанавливаем снова стартовое здоровье
        _enemyShip.health.GetStartHealth();

        // Увеличиваем количество очков для игрока

        // Изменяем спавн
        EnemySpawner.Instance.DecrementEnemyInScene();

        // Деактивируем
        _enemyShip.gameObject.SetActive(false);
    }

}
