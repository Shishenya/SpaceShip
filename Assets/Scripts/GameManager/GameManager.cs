using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

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
    [SerializeField] private SpriteRenderer background;

    [SerializeField] private CanvasGroup canvasFade;
    private string _textFade;

    [SerializeField] private List<LevelDetailsSO> levelDetailsList; // Список уровней
    public int currentLevelIndex = 0;

    [Header("SOUND PARAMETERS")]
    [Space(10)]
    public AudioMixerGroup soundsMasterMixerGroup;
    public AudioMixerGroup musicMasterMixerGroup;

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

    private void Start()
    {
        // StartCoroutine(Fade(1f, 0f, 2f, Color.black, _textFade));
    }

    /// <summary>
    /// Метод создание корабля игрока
    /// </summary>
    private void InitialisePlayerShip()
    {
        // Инициализируем игрока
        _playerShip = Instantiate(shipDetailsPlayer.prefabShip); // создаем объект
        _playerShip.GetComponent<Player>().SetStartPosition();
        //_playerShip.transform.position = new Vector3(Settings.startPlayerPosition.x, Settings.startPlayerPosition.y, 0f); // устанавливаем кординаты

        // Устанавливаем SO корабля
        Ship playerShipComponent = _playerShip.GetComponent<Ship>();
        playerShipComponent.InitShip(shipDetailsPlayer);


    }

    /// <summary>
    /// Инициализация уровня
    /// </summary>
    private void InitialiseLevel(int currentLevelIndex)
    {
        currentLevel = levelDetailsList[currentLevelIndex];
        background.sprite = levelDetailsList[currentLevelIndex].backgroundLevel; // обнвляем бекграунд
        _textFade = "Уровень " + (currentLevelIndex+1).ToString() + "\n " + levelDetailsList[currentLevelIndex].nameLevel;
        StartCoroutine(Fade(1f, 0f, 2f, Color.black, _textFade));
        _playerShip.GetComponent<Player>().SetStartPosition();
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
        PoolManager.Instance.ClearEnableList();
        StartCoroutine(GameLostRoutine());
    }

    /// <summary>
    /// Победа в игре
    /// </summary>
    public void GameWin()
    {
        PoolManager.Instance.ClearEnableList();
        StartCoroutine(GameWinRoutine());
    }

    /// <summary>
    /// Следующий уровень в игре
    /// </summary>
    public void GameNextLevel()
    {
        Debug.Log("Уровень пройден");
        currentLevelIndex++;
        PoolManager.Instance.ClearEnableList();

        if (currentLevelIndex>=levelDetailsList.Count)
        {
            GameWin();
        }
        else
        {
            InitialiseLevel(currentLevelIndex); // инициализируем новый уровень
            EnemySpawner.Instance.ResetLevel(); // Устанавливаем нвоый уровень
            MusicManager.Instance.RestartMusic();
            playerScoreUI.UpdateScore(); // обновляем UI 
        }
    }

    /// <summary>
    /// Затухание экрана и его появление
    /// </summary>
    public IEnumerator Fade(float startFadeAlpha, float targetFadeAlpha, float fadeSeconds, Color backgroundColor, string textFade)
    {

        Image image = canvasFade.GetComponent<Image>();
        image.color = backgroundColor;
        TextMeshProUGUI textTMP = canvasFade.GetComponentInChildren<TextMeshProUGUI>();
        textTMP.text = textFade;

        float time = 0;

        while (time <= fadeSeconds)
        {
            time += Time.deltaTime;
            canvasFade.alpha = Mathf.Lerp(startFadeAlpha, targetFadeAlpha, time / fadeSeconds);
            yield return null;
        }

    }

    /// <summary>
    /// Корутина с поражением
    /// </summary>
    public IEnumerator GameLostRoutine()
    {
        // Отключаем управление
        GetPlayerShip().GetComponent<PlayerController>().DisablePLayerContoller();

        // Запусуаем экран с поржанием
        _textFade = "Вы проиграли.";
        StartCoroutine(Fade(0f, 1f, 0f, Color.black, _textFade));
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Корутина с победой
    /// </summary>
    public IEnumerator GameWinRoutine()
    {
        // Отключаем управление
        GetPlayerShip().GetComponent<PlayerController>().DisablePLayerContoller();

        // Запусуаем экран с победой
        _textFade = "Вы выиграли!";
        StartCoroutine(Fade(0f, 1f, 0f, Color.black, _textFade));
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
