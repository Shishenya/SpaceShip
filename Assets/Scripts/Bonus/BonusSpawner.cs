using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> bonusList;
    [SerializeField] private int scoreBonusInstantiate = 20;
    [SerializeField] private float bonusLifeSecond = 2f;

    private float _minX = -10f;
    private float _maxX = 10f;
    private float _minY = -5f;
    private float _maxY = 5f;

    private int counterScore = 0;

    private void Awake()
    {
        IncrementNextBonusScoreInstantate();
    }

    private void Update()
    {
        if (GameManager.Instance.GameScore >= counterScore)
        {
            IncrementNextBonusScoreInstantate(); // увеличиаем счетчик для появления нового бонус
            InstantiateBonusInScene(); // добавляем бонус на сцену
        }
    }

    /// <summary>
    /// Появление бонуса на сцене
    /// </summary>
    private void InstantiateBonusInScene()
    {
        StartCoroutine(InstantiateBonusInSceneRoutine());
    }

    /// <summary>
    /// Корутина появления бонусы на сцене
    /// </summary>
    /// <returns></returns>
    private IEnumerator InstantiateBonusInSceneRoutine()
    {
        // достаем рандомный бонус
        GameObject randomPrefab = GetRandomBonusPrefab();

        // Достаем обэейкт из пула
        GameObject goSpawn = PoolManager.Instance.GetFromThePool(randomPrefab); // достаем объейкт из пула

        if (goSpawn != null)
        {
            // Случайная позиция для бонуса
            Vector2 randomVector2 = RandomPositionBonus();
            // Устанавливаем его
            goSpawn.transform.position = randomVector2;
            // делаем активным
            goSpawn.SetActive(true);


            yield return new WaitForSeconds(bonusLifeSecond);
            goSpawn.SetActive(false);
        }

        yield return null;

    }

    /// <summary>
    /// Установка значения, при котором бонус появится на сцене
    /// </summary>
    private void IncrementNextBonusScoreInstantate()
    {
        counterScore += scoreBonusInstantiate;
    }


    /// <summary>
    /// Случайная позиция для вектора
    /// </summary>
    private Vector2 RandomPositionBonus()
    {
        float x = Random.Range(_minX, _maxX);
        float y = Random.Range(_minY, _maxY);
        return new Vector2(x, y);
    }

    /// <summary>
    /// Возвращает случайный бонус
    /// </summary>
    private GameObject GetRandomBonusPrefab()
    {
        if (bonusList.Count > 0)
        {
            int index = Random.Range(0, bonusList.Count);
            return bonusList[index];
        }
        else
        {
            return null;
        }
    }

}
