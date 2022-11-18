using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSpawner : Singleton<EnvironmentSpawner>
{
    #region Meteors Spawn Parameters
    [Header("Meteors Spawn Parameters")]
    [Space(10)]
    #endregion
    [SerializeField] private List<GameObject> meteorsSpawnList;
    [SerializeField] private float meteorSpawnSeconds = 3f;

    #region Planet No collision Spawn Parameters
    [Header("Meteors Spawn Parameters")]
    [Space(10)]
    #endregion
    [SerializeField] private List<GameObject> planetsSpawnList;
    [SerializeField] private float planetSpawnSeconds = 8f;


    private float _timerMeteorSpawn;
    private float _timerPlanetSpawn;

    protected override void Awake()
    {
        base.Awake();

        _timerMeteorSpawn = meteorSpawnSeconds;
        _timerPlanetSpawn = planetSpawnSeconds / 2f;

    }

    private void Update()
    {
        MeteorTimerSpawn(); // спавн метеоритов
        PlanetTimerSpawn(); // спавн планет для фона
    }

    /// <summary>
    /// Достаемт случайный ГО из списка
    /// </summary>
    private GameObject GetRandomObjectInList(List<GameObject> listGO)
    {
        if (listGO.Count <= 0) return null;

        int index = Random.Range(0, listGO.Count);
        return listGO[index];

    }

    /// <summary>
    /// Таймер Спавна метеоритов
    /// </summary>
    private void MeteorTimerSpawn()
    {
        if (_timerMeteorSpawn >= 0)
        {
            _timerMeteorSpawn -= Time.deltaTime;
        }
        else
        {
            _timerMeteorSpawn = meteorSpawnSeconds;
            SpawnMeteor();
        }
    }

    /// <summary>
    /// Спав метеорита
    /// </summary>
    private void SpawnMeteor()
    {
        GameObject randomGO = GetRandomObjectInList(meteorsSpawnList);

        GameObject spawnObject = PoolManager.Instance.GetFromThePool(randomGO);
        if (spawnObject!=null)
        {
            Vector3 randomVector3 = new Vector3(10f, Random.Range(-5f, 5f), 0f);
            spawnObject.transform.position = randomVector3;
            spawnObject.SetActive(true);
        }

    }

    /// <summary>
    /// Таймер спавна планеты
    /// </summary>
    private void PlanetTimerSpawn()
    {
        if (_timerPlanetSpawn >= 0)
        {
            _timerPlanetSpawn -= Time.deltaTime;
        }
        else
        {
            _timerPlanetSpawn = planetSpawnSeconds;
            SpawnPlanet();
        }
    }

    /// <summary>
    /// Спавн планеты
    /// </summary>
    private void SpawnPlanet()
    {
        GameObject randomGO = GetRandomObjectInList(planetsSpawnList);

        GameObject spawnObject = PoolManager.Instance.GetFromThePool(randomGO);
        if (spawnObject != null)
        {
            Vector3 randomVector3 = new Vector3(10f, Random.Range(-5f, 5f), 0f);
            spawnObject.transform.position = randomVector3;
            spawnObject.SetActive(true);
        }
    }

}
