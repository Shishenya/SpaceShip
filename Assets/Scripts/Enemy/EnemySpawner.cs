using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Singleton<EnemySpawner>
{

    [SerializeField] private int countSpawnerEnemy = 3;
    [SerializeField] private ShipDetailsSO enemyShipDetails = null;

    private int _currentEnemyInScene = 0;
    private int _amountEnemySpawn = 0;
    private LevelDetailsSO _levelDetailsSO = null;
    private float _timerSpawn;
    private float _startTimerSpawn;
    private bool _readySpawn = true;

    public int CurrentEnemyInScene { get { return _currentEnemyInScene; } }


    [SerializeField] private GameObject _testEnemyPrefab = null;

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
            Spawn(_testEnemyPrefab);
        }

        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    Spawn(_testEnemyPrefab);
        //}

    }

    /// <summary>
    /// ����� ����������
    /// </summary>
    private void Spawn(GameObject _testEnemyPrefab)
    {

        // ��������� �� ������� ���������� ������������ �����������

        // ���� �� ����� � ������
        if (!_readySpawn) return;

        // ���� ��� ������������ ������ ������ ��� ����� �� ��� �����
        if (_amountEnemySpawn >= _levelDetailsSO.amountEnemyInLevel) return;

        // ��� �� ����� ��� ���� ������� � ��������
        if (_currentEnemyInScene >= _levelDetailsSO.maxEnemyConcurent) return;

        Debug.Log("����� � ������!");

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

            // ������������� ������ �������
            Ship enemyShip = goSpawn.GetComponent<Ship>();
            if (enemyShip != null)
                enemyShip.InitShip(enemyShipDetails);
        }

        _timerSpawn = _startTimerSpawn; // ������������� ������ ������ � ��������� ��������
        _readySpawn = false; // ������ �������� �� �����

    }

    /// <summary>
    /// ����� ���������
    /// </summary>
    public void ResetLevel()
    {
        _levelDetailsSO = GameManager.Instance.currentLevel; // ����� �������
        _startTimerSpawn = _levelDetailsSO.intervalEnemySpawner; // ������������� ��������� �����
        _timerSpawn = _startTimerSpawn; // ������������ ������ ������
        _currentEnemyInScene = 0; // �� ����� ����������� 0
        _amountEnemySpawn = 0; // ����� ������������ 0
    }

    /// <summary>
    /// ���������� �������� ���������� ������ �� ����� �� ������ ��� ��� �����������
    /// </summary>
    public void DecrementEnemyInScene()
    {
        _currentEnemyInScene--;
        if (_currentEnemyInScene <= 0) _currentEnemyInScene = 0;
    }

    /// <summary>
    /// ���������� ����� ������, ������� ��� ������ ������������
    /// </summary>
    /// <returns></returns>
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
