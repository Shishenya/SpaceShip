using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    #region Header Player Settings
    [Space(10)]
    [Header("Player Settings")]
    #endregion
    [Tooltip("Player ship details")]
    [SerializeField] private ShipDetailsSO shipDetailsPlayer;

    private GameObject _playerShip = null; // Корабль игрока

    #region Test
    public LevelDetailsSO currentLevel = null;
    #endregion

    protected override void Awake()
    {
        base.Awake();

        // Инициализируем игрока
        InitialisePlayerShip();
    }

    /// <summary>
    /// Метод создание корабля игрока
    /// </summary>
    private void InitialisePlayerShip()
    {
        // Инициализируем игрока
        _playerShip = Instantiate(shipDetailsPlayer.prefabShip); // создаем объект
        _playerShip.transform.position = new Vector3(Settings.startPlayerPosition.x, Settings.startPlayerPosition.y, 0f); // устанавливаем кординаты

        // Устанавливаем SO корабля
        Ship playerShipDetails = _playerShip.GetComponent<Ship>();
        // playerShipDetails._currentShipDetails = shipDetailsPlayer;
        playerShipDetails.InitShip(shipDetailsPlayer);


    }

    /// <summary>
    /// Возвращает ссылку на корабль игрока
    /// </summary>
    public GameObject GetPlayerShip()
    {
        return _playerShip;
    }

}
