using UnityEngine;

public class Player : MonoBehaviour, IDeadable
{
    private Ship _playerShip;

    private void OnEnable()
    {
        _playerShip.deathEvent.OnDeath += DeathEvent_OnDeath;
    }

    private void OnDisable()
    {
        _playerShip.deathEvent.OnDeath -= DeathEvent_OnDeath;
    }

    private void Awake()
    {
        _playerShip = GetComponent<Ship>();
    }

    public void DeathEvent_OnDeath()
    {
        Death();
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
    public void Death()
    {
        GameManager.Instance.GameLost();  // перезапускаем сцену
    }

}
