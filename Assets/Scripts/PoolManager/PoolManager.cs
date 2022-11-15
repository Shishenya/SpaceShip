using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    [SerializeField] PoolArray[] poolArray = null;
    private Transform parrentTransformAllPull;
    private Dictionary<int, Queue<GameObject>> poolDictionary = new Dictionary<int, Queue<GameObject>>();

    #region Test
    [SerializeField] private GameObject testPrefab;
    #endregion

    // Структура для пула
    [System.Serializable]
    struct PoolArray
    {
        public int sizePool; // Количество
        public GameObject prefabPool; // префаб
    }

    private void Start()
    {
        parrentTransformAllPull = this.gameObject.transform; // Задаем родителя для всех обейктов

        // Создаем пул
        for (int i = 0; i < poolArray.Length; i++)
        {
            CreatePool(poolArray[i].sizePool, poolArray[i].prefabPool);
        }

    }

    /// <summary>
    /// Создание пула объектов
    /// </summary>
    /// <param name="poolSize"></param>
    /// <param name="poolPrefab"></param>
    private void CreatePool(int poolSize, GameObject poolPrefab)
    {

        int keyID = poolPrefab.GetInstanceID(); // получаем ключ префаба

        string prefabName = poolPrefab.name; // Имя префаба

        GameObject parentGameObject = new GameObject(prefabName + "Anchor"); // создаем родителя для группы префабов пула

        parentGameObject.transform.SetParent(parrentTransformAllPull); // устанавлиаем ему родителя наш пул менеджер

        // если ключа нет в словаре
        if (!poolDictionary.ContainsKey(keyID))
        {
            poolDictionary.Add(keyID, new Queue<GameObject>()); // добавляем его

            for (int i = 0; i < poolSize; i++) // добавляем все объйекты в него
            {
                GameObject newObject = Instantiate(poolPrefab, parentGameObject.transform) as GameObject; // создаем объейкт
                 
                newObject.SetActive(false); // выключаем его

                poolDictionary[keyID].Enqueue(newObject); // добавялем в словарь

            }
        }

    }

    /// <summary>
    /// Метод достает объйект из пула и возвращает ссылку на него
    /// </summary>
    /// <returns></returns>
    public GameObject GetFromThePool(GameObject prefab)
    {
        int keyID = prefab.GetInstanceID();
        if (poolDictionary.ContainsKey(keyID))
        {
            // Debug.Log("Пул данных объектов найден");
            GameObject go = poolDictionary[keyID].Dequeue();
            poolDictionary[keyID].Enqueue(go);

            // Если объейкт был активен, то выключаем его
            if (go.gameObject.activeSelf == true)
            {
                go.gameObject.SetActive(false);
            }

            return go;
        }
        else
        {
            Debug.Log("Ошибка. Такого пула нет");
        }

        return null;

    }

    private void ResetPrefab(GameObject prefab)
    {
        prefab.transform.position = transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameObject go = GetFromThePool(testPrefab);
            if (go!=null)
            {
                ResetPrefab(go);
                go.SetActive(true);
            }
        }
    }

}
