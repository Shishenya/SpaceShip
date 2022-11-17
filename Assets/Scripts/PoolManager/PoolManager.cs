using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    [SerializeField] PoolArray[] poolArray = null;
    private Transform parrentTransformAllPull;
    private Dictionary<int, Queue<GameObject>> poolDictionary = new Dictionary<int, Queue<GameObject>>();

    #region test
    // test
    private List<GameObject> _enableGOList = new List<GameObject>();
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
            _enableGOList.Add(go); // test

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


    /// <summary>
    /// Очищает с экрана элементы после загрузки уровня
    /// </summary>
    public void ClearEnableList()
    {
        for (int i = _enableGOList.Count - 1; i >= 0; i--)
        {
            GameObject go = _enableGOList[i];
            go.SetActive(false);
            _enableGOList.RemoveAt(i);
        }
        _enableGOList.Clear();
    }

    /// <summary>
    /// Удаляет из списка пула на экрана GameObject 
    /// </summary>
    public void DeleteFromEnableList(GameObject gameObject)
    {
        if (_enableGOList.Contains(gameObject))
        {
            int index = _enableGOList.IndexOf(gameObject);
            _enableGOList.RemoveAt(index);
        }
    }

}
