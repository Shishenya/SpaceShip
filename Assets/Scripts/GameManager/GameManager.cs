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

    #region Header UI Element
    [Space(10)]
    [Header("UI Element")]
    #endregion
    public PlayerScoreUI playerScoreUI;
    [SerializeField] private SpriteRenderer _background;
    [SerializeField] private CanvasGroup _canvasFade;
    [SerializeField] private GameObject _pauseMenuUI;

    private GameObject _playerShip = null; // Корабль игрока
    private string _textFade; // текст соообщения при появлении экрана
    private bool _isPause = false;

    public bool IsPause
    {
        get => _isPause;
        set => _isPause = value;
    }

    #region Level Parameters
    [Space(10)]
    [Header("Level Parameters")]
    #endregion
    [Tooltip("Level list in game")]
    [SerializeField] private List<LevelDetailsSO> _levelDetailsList; // Список уровней
    [HideInInspector] public int currentLevelIndex = 0;
    [HideInInspector] public LevelDetailsSO currentLevelDetails = null;

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


    protected override void Awake()
    {
        base.Awake();

        // Скрываем меню паузы
        _pauseMenuUI.SetActive(false);

        // Инициализируем игрока
        InitialisePlayerShip();

        // Инициализируем уровень
        InitialiseLevel(currentLevelIndex);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPause)
            {
                _playerShip.GetComponent<PlayerController>().EnablePLayerContoller();
                _pauseMenuUI.SetActive(false);
                _isPause = false;
            }
            else
            {
                _playerShip.GetComponent<PlayerController>().DisablePLayerContoller();
                _pauseMenuUI.SetActive(true);
                _isPause = true;
            }
        }
    }

    /// <summary>
    /// Метод создание корабля игрока
    /// </summary>
    private void InitialisePlayerShip()
    {
        // Инициализируем игрока
        _playerShip = Instantiate(shipDetailsPlayer.prefabShip); // создаем объект
        _playerShip.GetComponent<Player>().SetStartPosition();

        // Устанавливаем SO корабля
        Ship playerShipComponent = _playerShip.GetComponent<Ship>();
        playerShipComponent.InitShip(shipDetailsPlayer);


    }

    /// <summary>
    /// Инициализация уровня
    /// </summary>
    private void InitialiseLevel(int currentLevelIndex)
    {
        currentLevelDetails = _levelDetailsList[currentLevelIndex];
        _background.sprite = _levelDetailsList[currentLevelIndex].backgroundLevel; // обнвляем бекграунд
        _textFade = "Уровень " + (currentLevelIndex + 1).ToString() + "\n " + _levelDetailsList[currentLevelIndex].nameLevel;
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
        currentLevelIndex++;
        PoolManager.Instance.ClearEnableList();

        if (currentLevelIndex >= _levelDetailsList.Count)
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

        Image image = _canvasFade.GetComponent<Image>();
        image.color = backgroundColor;
        TextMeshProUGUI textTMP = _canvasFade.GetComponentInChildren<TextMeshProUGUI>();
        textTMP.text = textFade;

        float time = 0;

        while (time <= fadeSeconds)
        {
            time += Time.deltaTime;
            _canvasFade.alpha = Mathf.Lerp(startFadeAlpha, targetFadeAlpha, time / fadeSeconds);
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
