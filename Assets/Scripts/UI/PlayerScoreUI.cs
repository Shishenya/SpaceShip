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
        _scoreTMP.text = "Score: " +  GameManager.Instance.GameScore.ToString() + "\nEnemy Remains: "+EnemySpawner.Instance.GetRemainsToSpawnEnemy().ToString();
    }

}
