using System.Collections;
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


    private float _timerMeteorSpawn;

    protected override void Awake()
    {
        base.Awake();

        _timerMeteorSpawn = meteorSpawnSeconds;

    }

    private void Update()
    {
        MeteorTimerSpawn(); // спавн метеоритов
    }

    /// <summary>
    /// Достаемт случайный ГО из списка
    /// </summary>
    /// <param name="listGO"></param>
    /// <returns></returns>
    private GameObject GetRandomObjectInList(List<GameObject> listGO)
    {
        if (listGO.Count <= 0) return null;

        int index = Random.Range(0, listGO.Count);
        return listGO[index];

    }

    /// <summary>
    /// Спавм метеоритов
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

}
