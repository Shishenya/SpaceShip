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


    [SerializeField] private GameObject _testEnemyPrefab = null;

    private void Start()
    {
        _levelDetailsSO = GameManager.Instance.currentLevel;
        _startTimerSpawn = _levelDetailsSO.intervalEnemySpawner;
        _timerSpawn = _startTimerSpawn;
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

        if (Input.GetKeyDown(KeyCode.O))
        {
            Spawn(_testEnemyPrefab);
        }

    }

    /// <summary>
    /// ����� ����������
    /// </summary>
    private void Spawn(GameObject _testEnemyPrefab)
    {
        Debug.Log("������� ���!");

        // ��������� �� ������� ���������� ������������ �����������

        // ���� �� ����� � ������
        if (!_readySpawn) return;

        // ���� ��� ������������ ������ ������ ��� ����� �� ��� �����
        if (_amountEnemySpawn >= _levelDetailsSO.amountEnemyInLevel) return;

        // ��� �� ����� ��� ���� ������� � ��������
        if (_currentEnemyInScene >= _levelDetailsSO.maxEnemyConcurent) return;

        Debug.Log("����� � ������!");

        // ���� ���, �� ������� ������� � ���������� ��������
        _amountEnemySpawn++;
        _currentEnemyInScene++;

        Debug.Log("������ ���������� �����;  ����� ����������� " + _amountEnemySpawn + "; �� ����� ������ " + _currentEnemyInScene);

        GameObject goSpawn = PoolManager.Instance.GetFromThePool(_testEnemyPrefab); // ������� ������� �� ����

        // Test
        Vector3 randomVector3 = new Vector3(10f, Random.Range(-5f, 5f),0f);
        goSpawn.transform.position = randomVector3;
        goSpawn.SetActive(true);

        // ������������� ������ �������
        Ship enemyShip = goSpawn.GetComponent<Ship>();
        enemyShip.InitShip(enemyShipDetails);


        _timerSpawn = _startTimerSpawn; // ������������� ������ ������ � ��������� ��������
        _readySpawn = false; // ������ �������� �� �����

    }

    /// <summary>
    /// ����� ���������
    /// </summary>
    private void ResetCount()
    {
        _currentEnemyInScene = 0;
        _amountEnemySpawn = 0;
    }

    /// <summary>
    /// ���������� �������� ���������� ������ �� ����� �� ������ ��� ��� �����������
    /// </summary>
    public void DecrementEnemyInScene()
    {
        _currentEnemyInScene--;
        if (_currentEnemyInScene <= 0) _currentEnemyInScene = 0;
    }

}
