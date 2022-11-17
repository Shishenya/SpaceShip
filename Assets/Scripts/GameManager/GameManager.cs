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
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private CanvasGroup canvasFade;
    [SerializeField] private GameObject pauseMenuUI;

    private GameObject _playerShip = null; // ������� ������
    private string _textFade; // ����� ���������� ��� ��������� ������
    private bool _isPause = false;

    public bool IsPause
    {
        get => _isPause;
        set => _isPause = value;
    }

    [SerializeField] private List<LevelDetailsSO> levelDetailsList; // ������ �������
    [HideInInspector] public int currentLevelIndex = 0;

    [Header("SOUND PARAMETERS")]
    [Space(10)]
    public AudioMixerGroup soundsMasterMixerGroup;
    public AudioMixerGroup musicMasterMixerGroup;

    [HideInInspector] public LevelDetailsSO currentLevel = null;

    private int _gameScore; // ������� ����
    public int GameScore
    {
        get { return _gameScore; }
        set { _gameScore += value; }
    }


    protected override void Awake()
    {
        base.Awake();

        // �������� ���� �����
        pauseMenuUI.SetActive(false);

        // �������������� ������
        InitialisePlayerShip();

        // �������������� �������
        InitialiseLevel(currentLevelIndex);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPause)
            {
                _playerShip.GetComponent<PlayerController>().EnablePLayerContoller();
                pauseMenuUI.SetActive(false);
                _isPause = false;
            }
            else
            {
                _playerShip.GetComponent<PlayerController>().DisablePLayerContoller();
                pauseMenuUI.SetActive(true);
                _isPause = true;
            }
        }
    }

    /// <summary>
    /// ����� �������� ������� ������
    /// </summary>
    private void InitialisePlayerShip()
    {
        // �������������� ������
        _playerShip = Instantiate(shipDetailsPlayer.prefabShip); // ������� ������
        _playerShip.GetComponent<Player>().SetStartPosition();
        //_playerShip.transform.position = new Vector3(Settings.startPlayerPosition.x, Settings.startPlayerPosition.y, 0f); // ������������� ���������

        // ������������� SO �������
        Ship playerShipComponent = _playerShip.GetComponent<Ship>();
        playerShipComponent.InitShip(shipDetailsPlayer);


    }

    /// <summary>
    /// ������������� ������
    /// </summary>
    private void InitialiseLevel(int currentLevelIndex)
    {
        currentLevel = levelDetailsList[currentLevelIndex];
        background.sprite = levelDetailsList[currentLevelIndex].backgroundLevel; // �������� ���������
        _textFade = "������� " + (currentLevelIndex + 1).ToString() + "\n " + levelDetailsList[currentLevelIndex].nameLevel;
        StartCoroutine(Fade(1f, 0f, 2f, Color.black, _textFade));
        _playerShip.GetComponent<Player>().SetStartPosition();
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
        PoolManager.Instance.ClearEnableList();
        StartCoroutine(GameLostRoutine());
    }

    /// <summary>
    /// ������ � ����
    /// </summary>
    public void GameWin()
    {
        PoolManager.Instance.ClearEnableList();
        StartCoroutine(GameWinRoutine());
    }

    /// <summary>
    /// ��������� ������� � ����
    /// </summary>
    public void GameNextLevel()
    {
        Debug.Log("������� �������");
        currentLevelIndex++;
        PoolManager.Instance.ClearEnableList();

        if (currentLevelIndex >= levelDetailsList.Count)
        {
            GameWin();
        }
        else
        {
            InitialiseLevel(currentLevelIndex); // �������������� ����� �������
            EnemySpawner.Instance.ResetLevel(); // ������������� ����� �������
            MusicManager.Instance.RestartMusic();
            playerScoreUI.UpdateScore(); // ��������� UI 
        }
    }

    /// <summary>
    /// ��������� ������ � ��� ���������
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
    /// �������� � ����������
    /// </summary>
    public IEnumerator GameLostRoutine()
    {
        // ��������� ����������
        GetPlayerShip().GetComponent<PlayerController>().DisablePLayerContoller();

        // ��������� ����� � ���������
        _textFade = "�� ���������.";
        StartCoroutine(Fade(0f, 1f, 0f, Color.black, _textFade));
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// �������� � �������
    /// </summary>
    public IEnumerator GameWinRoutine()
    {
        // ��������� ����������
        GetPlayerShip().GetComponent<PlayerController>().DisablePLayerContoller();

        // ��������� ����� � �������
        _textFade = "�� ��������!";
        StartCoroutine(Fade(0f, 1f, 0f, Color.black, _textFade));
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
