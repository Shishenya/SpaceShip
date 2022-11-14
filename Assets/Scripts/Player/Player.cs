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
    /// ������� �� ��������� �������
    /// </summary>
    private void DeathPlayer()
    {
        Debug.Log("����� ��������!");
        GameManager.Instance.GameLost();  // ������������� �����
    }

}