using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreTMP;


    private void Start()
    {
        UpdateScore();
    }

    public void UpdateScore()
    {
        _scoreTMP.text = "Level:" +(GameManager.Instance.currentLevelIndex+1).ToString()+ 
                         "\nScore: " +  GameManager.Instance.GameScore.ToString() +
                         "\nВсего врагов на уровне: " + EnemySpawner.Instance.AmountEnemyInScene.ToString() +
                         "\nОжидающих спавна: " +EnemySpawner.Instance.GetRemainsToSpawnEnemy().ToString() +
                         "\nНа сцене врагов: " + EnemySpawner.Instance.CurrentEnemyInScene.ToString() + 
                         "\nУбито врагов: " + EnemySpawner.Instance.testDeathEnemy.ToString();
    }

}
