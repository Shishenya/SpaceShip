using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    [SerializeField] PoolArray[] poolArray = null;
    private Transform parrentTransformAllPull;
    private Dictionary<int, Queue<GameObject>> poolDictionary = new Dictionary<int, Queue<GameObject>>();

    // ��������� ��� ����
    [System.Serializable]
    struct PoolArray
    {
        public int sizePool; // ����������
        public GameObject prefabPool; // ������
    }

    private void Start()
    {
        parrentTransformAllPull = this.gameObject.transform; // ������ �������� ��� ���� ��������

        // ������� ���
        for (int i = 0; i < poolArray.Length; i++)
        {
            CreatePool(poolArray[i].sizePool, poolArray[i].prefabPool);
        }

    }

    /// <summary>
    /// �������� ���� ��������
    /// </summary>
    /// <param name="poolSize"></param>
    /// <param name="poolPrefab"></param>
    private void CreatePool(int poolSize, GameObject poolPrefab)
    {

        int keyID = poolPrefab.GetInstanceID(); // �������� ���� �������

        string prefabName = poolPrefab.name; // ��� �������

        GameObject parentGameObject = new GameObject(prefabName + "Anchor"); // ������� �������� ��� ������ �������� ����

        parentGameObject.transform.SetParent(parrentTransformAllPull); // ������������ ��� �������� ��� ��� ��������

        // ���� ����� ��� � �������
        if (!poolDictionary.ContainsKey(keyID))
        {
            poolDictionary.Add(keyID, new Queue<GameObject>()); // ��������� ���

            for (int i = 0; i < poolSize; i++) // ��������� ��� �������� � ����
            {
                GameObject newObject = Instantiate(poolPrefab, parentGameObject.transform) as GameObject; // ������� �������
                 
                newObject.SetActive(false); // ��������� ���

                poolDictionary[keyID].Enqueue(newObject); // ��������� � �������

            }
        }

    }

    /// <summary>
    /// ����� ������� ������� �� ���� � ���������� ������ �� ����
    /// </summary>
    /// <returns></returns>
    public GameObject GetFromThePool(GameObject prefab)
    {
        int keyID = prefab.GetInstanceID();
        if (poolDictionary.ContainsKey(keyID))
        {
            // Debug.Log("��� ������ �������� ������");
            GameObject go = poolDictionary[keyID].Dequeue();
            poolDictionary[keyID].Enqueue(go);

            // ���� ������� ��� �������, �� ��������� ���
            if (go.gameObject.activeSelf == true)
            {
                go.gameObject.SetActive(false);
            }

            return go;
        }
        else
        {
            Debug.Log("������. ������ ���� ���");
        }

        return null;

    }

    private void ResetPrefab(GameObject prefab)
    {
        prefab.transform.position = transform.position;
    }

}
