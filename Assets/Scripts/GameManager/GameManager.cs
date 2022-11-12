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

    private GameObject _playerShip = null; // ������� ������

    #region Test
    public LevelDetailsSO currentLevel = null;
    #endregion

    protected override void Awake()
    {
        base.Awake();

        // �������������� ������
        InitialisePlayerShip();
    }

    /// <summary>
    /// ����� �������� ������� ������
    /// </summary>
    private void InitialisePlayerShip()
    {
        // �������������� ������
        _playerShip = Instantiate(shipDetailsPlayer.prefabShip); // ������� ������
        _playerShip.transform.position = new Vector3(Settings.startPlayerPosition.x, Settings.startPlayerPosition.y, 0f); // ������������� ���������

        // ������������� SO �������
        Ship playerShipDetails = _playerShip.GetComponent<Ship>();
        // playerShipDetails._currentShipDetails = shipDetailsPlayer;
        playerShipDetails.InitShip(shipDetailsPlayer);


    }

    /// <summary>
    /// ���������� ������ �� ������� ������
    /// </summary>
    public GameObject GetPlayerShip()
    {
        return _playerShip;
    }

}
