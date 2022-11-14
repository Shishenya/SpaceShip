using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    #region Header Player Settings
    [Space(10)]
    [Header("Player Settings")]
    #endregion
    [Tooltip("Player ship details")]
    [SerializeField] private ShipDetailsSO shipDetailsPlayer;

    private GameObject _playerShip = null; // ������� ������

    public PlayerScoreUI playerScoreUI;

    [SerializeField] private List<LevelDetailsSO> levelDetailsList; // ������ �������
    public int currentLevelIndex = 0;

    private int _gameScore; // ������� ����
    public int GameScore
    {
        get { return _gameScore; }
        set { _gameScore += value; }
    }


    #region Test
    public LevelDetailsSO currentLevel = null;
    #endregion

    protected override void Awake()
    {
        base.Awake();

        // �������������� ������
        InitialisePlayerShip();

        // �������������� �������
        InitialiseLevel(currentLevelIndex);
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
        Ship playerShipComponent = _playerShip.GetComponent<Ship>();
        playerShipComponent.InitShip(shipDetailsPlayer);


    }

    /// <summary>
    /// ������������� ������
    /// </summary>
    /// <param name="levelDetails"></param>
    private void InitialiseLevel(int currentLevelIndex)
    {
        currentLevel = levelDetailsList[currentLevelIndex];
    }

    /// <summary>
    /// ���������� ������ �� ������� ������
    /// </summary>
    public GameObject GetPlayerShip()
    {
        return _playerShip;
    }

    #region Game Status
    #endregion

    /// <summary>
    /// ��������� � ����
    /// </summary>
    public void GameLost()
    {
        // ������������� �����
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// ������ � ����
    /// </summary>
    public void GameWin()
    {
        Debug.Log("�� ������ ��������� �������. ����������!");
    }

    /// <summary>
    /// ��������� ������� � ����
    /// </summary>
    public void GameNextLevel()
    {
        Debug.Log("������� �������");
        currentLevelIndex++;

        if (currentLevelIndex> levelDetailsList.Count)
        {
            GameWin();
        }
        else
        {
            InitialiseLevel(currentLevelIndex); // �������������� ����� �������
            EnemySpawner.Instance.ResetLevel(); // ������������� ����� �������
            playerScoreUI.UpdateScore(); // ��������� UI 
        }
    }

}
