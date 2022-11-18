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
    /// Спавн противника
    /// </summary>
    private void Spawn()
    {

        // Валидация на текущее количество заспавненных противников

        // если не готов к спавну
        if (!_readySpawn) return;

        // если уже заспавнилось больше врагов чем нужно за все время
        if (_amountEnemySpawn >= _levelDetailsSO.amountEnemyInLevel) return;

        // или на сцене уже есть корабли в достатке
        if (_currentEnemyInScene >= _levelDetailsSO.maxEnemyConcurent) return;

        // Debug.Log("Готов к спавну!");

        // если нет, то спавник корабль и прибавляем счетчики

        // Получаем случайнфй префаб корабля врага
        GameObject randomPrefab = GetRandomEnemyBySpawnLevel();

        GameObject goSpawn = PoolManager.Instance.GetFromThePool(randomPrefab); // достаем объейкт из пула
        if (goSpawn != null)
        {
            _amountEnemySpawn++;
            _currentEnemyInScene++;

            Vector3 randomVector3 = new Vector3(10f, Random.Range(-5f, 5f), 0f);
            goSpawn.transform.position = randomVector3;
            goSpawn.SetActive(true);

        }

        _timerSpawn = _startTimerSpawn; // устанавливаем таймер спавна в начальное значение
        _readySpawn = false; // Ставим заглушку на спавн

        // Обновляем счетчик
        GameManager.Instance.playerScoreUI.UpdateScore();

    }

    /// <summary>
    /// Сброс счетчиков
    /// </summary>
    public void ResetLevel()
    {
        _levelDetailsSO = GameManager.Instance.currentLevelDetails; // берем уровень
        _startTimerSpawn = _levelDetailsSO.intervalEnemySpawner; // устанавливаем стартовое время
        _timerSpawn = _startTimerSpawn; // устаналиваем атймер спавна
        _currentEnemyInScene = 0; // на сцене противников 0
        _amountEnemySpawn = 0; // всего заспавнилось 0
        amountDeathEnemy = 0; // тестовое, сколько на уровне убил врагов
    }

    /// <summary>
    /// Уменьшение текущего количество врагов на сцене на одного при его уничтожении
    /// </summary>
    public void DecrementEnemyInScene()
    {
        _currentEnemyInScene--;
        amountDeathEnemy++; // TEST!
        if (_currentEnemyInScene <= 0) _currentEnemyInScene = 0;
    }

    /// <summary>
    /// Возвращает число врагов, которое еще должно заспавниться
    /// </summary>
    public int GetRemainsToSpawnEnemy()
    {
        return _levelDetailsSO.amountEnemyInLevel - _amountEnemySpawn;
    }


    /// <summary>
    /// ВОзвращает случайный префаб врага среди тех, которые могут появиться на уровне
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
