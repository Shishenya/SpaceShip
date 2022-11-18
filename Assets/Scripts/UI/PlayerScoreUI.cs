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
                         "\n����� ������ �� ������: " + EnemySpawner.Instance.AmountEnemyInScene.ToString() +
                         "\n��������� ������: " +EnemySpawner.Instance.GetRemainsToSpawnEnemy().ToString() +
                         "\n�� ����� ������: " + EnemySpawner.Instance.CurrentEnemyInScene.ToString() + 
                         "\n����� ������: " + EnemySpawner.Instance.amountDeathEnemy.ToString();
    }

}
