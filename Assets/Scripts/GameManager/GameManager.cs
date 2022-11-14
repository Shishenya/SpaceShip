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

    private GameObject _playerShip = null; // Корабль игрока

    public PlayerScoreUI playerScoreUI;

    [SerializeField] private List<LevelDetailsSO> levelDetailsList; // Список уровней
    public int currentLevelIndex = 0;

    private int _gameScore; // Игровые очки
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

        // Инициализируем игрока
        InitialisePlayerShip();

        // Инициализируем уровень
        InitialiseLevel(currentLevelIndex);
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
        Ship playerShipComponent = _playerShip.GetComponent<Ship>();
        playerShipComponent.InitShip(shipDetailsPlayer);


    }

    /// <summary>
    /// Инициализация уровня
    /// </summary>
    /// <param name="levelDetails"></param>
    private void InitialiseLevel(int currentLevelIndex)
    {
        currentLevel = levelDetailsList[currentLevelIndex];
    }

    /// <summary>
    /// Возвращает ссылку на корабль игрока
    /// </summary>
    public GameObject GetPlayerShip()
    {
        return _playerShip;
    }

    #region Game Status
    #endregion

    /// <summary>
    /// Поражение в игре
    /// </summary>
    public void GameLost()
    {
        // перезапускаем сцену
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Победа в игре
    /// </summary>
    public void GameWin()
    {
        Debug.Log("Вы прошли последний уровень. Поздравляю!");
    }

    /// <summary>
    /// Следующий уровень в игре
    /// </summary>
    public void GameNextLevel()
    {
        Debug.Log("Уровень пройден");
        currentLevelIndex++;

        if (currentLevelIndex> levelDetailsList.Count)
        {
            GameWin();
        }
        else
        {
            InitialiseLevel(currentLevelIndex); // инициализируем новый уровень
            EnemySpawner.Instance.ResetLevel(); // Устанавливаем нвоый уровень
            playerScoreUI.UpdateScore(); // обновляем UI 
        }
    }

}
