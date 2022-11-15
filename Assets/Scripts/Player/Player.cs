using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Ship _playerShip;

    private void OnEnable()
    {
        _playerShip.deathEvent.OnDeath += DeathEvent_OnDeathPlayer;
    }

    private void OnDisable()
    {
        _playerShip.deathEvent.OnDeath -= DeathEvent_OnDeathPlayer;
    }

    private void Awake()
    {
        _playerShip = GetComponent<Ship>();
    }

    private void DeathEvent_OnDeathPlayer(DeathEventArgs deathEventArgs)
    {
        DeathPlayer();
    }

    /// <summary>
    /// Устанавливаеи стартовую позицию игрока
    /// </summary>
    public void SetStartPosition()
    {
        transform.position = new Vector3(Settings.startPlayerPosition.x, Settings.startPlayerPosition.y, 0f);
    }

    /// <summary>
    /// реакция на поражение игррока
    /// </summary>
    private void DeathPlayer()
    {
        Debug.Log("Игрок проиграл!");
        GameManager.Instance.GameLost();  // перезапускаем сцену
    }

}
