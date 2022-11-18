using UnityEngine;

public class EnemySpawner : Singleton<EnemySpawner>
{

    private int _currentEnemyInScene = 0;
    private int _amountEnemySpawn = 0;
    private LevelDetailsSO _levelDetailsSO = null;
    private float _timerSpawn;
    private float _startTimerSpawn;
    private bool _readySpawn = true;
    [HideInInspector] public int amountDeathEnemy = 0;

    public int AmountEnemyInScene
    {
        get { return _levelDetailsSO.amountEnemyInLevel;  }
    }

    public int CurrentEnemyInScene { get { return _currentEnemyInScene; } }

    protected override void Awake()
    {

        base.Awake();

        ResetLevel();
    }

    private void Update()
    {
        if (!_readySpawn)
        {
            _timerSpawn -= Time.deltaTime;
            if (_timerSpawn < 0)
            {
                _readySpawn = true;
                _timerSpawn = _startTimerSpawn;
            }
        }
        else
        {
            Spawn();
        }

    }

    /// <summary>
    /// ����� ����������
    /// </summary>
    private void Spawn()
    {

        // ��������� �� ������� ���������� ������������ �����������

        // ���� �� ����� � ������
        if (!_readySpawn) return;

        // ���� ��� ������������ ������ ������ ��� ����� �� ��� �����
        if (_amountEnemySpawn >= _levelDetailsSO.amountEnemyInLevel) return;

        // ��� �� ����� ��� ���� ������� � ��������
        if (_currentEnemyInScene >= _levelDetailsSO.maxEnemyConcurent) return;

        // Debug.Log("����� � ������!");

        // ���� ���, �� ������� ������� � ���������� ��������

        // �������� ��������� ������ ������� �����
        GameObject randomPrefab = GetRandomEnemyBySpawnLevel();

        GameObject goSpawn = PoolManager.Instance.GetFromThePool(randomPrefab); // ������� ������� �� ����
        if (goSpawn != null)
        {
            _amountEnemySpawn++;
            _currentEnemyInScene++;

            Vector3 randomVector3 = new Vector3(10f, Random.Range(-5f, 5f), 0f);
            goSpawn.transform.position = randomVector3;
            goSpawn.SetActive(true);

        }

        _timerSpawn = _startTimerSpawn; // ������������� ������ ������ � ��������� ��������
        _readySpawn = false; // ������ �������� �� �����

        // ��������� �������
        GameManager.Instance.playerScoreUI.UpdateScore();

    }

    /// <summary>
    /// ����� ���������
    /// </summary>
    public void ResetLevel()
    {
        _levelDetailsSO = GameManager.Instance.currentLevelDetails; // ����� �������
        _startTimerSpawn = _levelDetailsSO.intervalEnemySpawner; // ������������� ��������� �����
        _timerSpawn = _startTimerSpawn; // ������������ ������ ������
        _currentEnemyInScene = 0; // �� ����� ����������� 0
        _amountEnemySpawn = 0; // ����� ������������ 0
        amountDeathEnemy = 0; // ��������, ������� �� ������ ���� ������
    }

    /// <summary>
    /// ���������� �������� ���������� ������ �� ����� �� ������ ��� ��� �����������
    /// </summary>
    public void DecrementEnemyInScene()
    {
        _currentEnemyInScene--;
        amountDeathEnemy++; // TEST!
        if (_currentEnemyInScene <= 0) _currentEnemyInScene = 0;
    }

    /// <summary>
    /// ���������� ����� ������, ������� ��� ������ ������������
    /// </summary>
    public int GetRemainsToSpawnEnemy()
    {
        return _levelDetailsSO.amountEnemyInLevel - _amountEnemySpawn;
    }


    /// <summary>
    /// ���������� ��������� ������ ����� ����� ���, ������� ����� ��������� �� ������
    /// </summary>
    private GameObject GetRandomEnemyBySpawnLevel()
    {
        int randomIndex = Random.Range(0, _levelDetailsSO.enemyGOList.Count);
        if (_levelDetailsSO.enemyGOList[randomIndex] != null)
        {
            return _levelDetailsSO.enemyGOList[randomIndex];
        }
        else
        {
            return null;

        }

    }

}
