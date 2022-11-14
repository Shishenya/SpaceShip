using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthTMP;
    private Health _health;
    private Ship _playerShip;
    private int _currentHealth;

    private void OnEnable()
    {
        _playerShip.changeHealthEvent.OnChangeHealth += ChangeHealthEvent_UpdateUIPlayerHealth;
    }

    private void OnDisable()
    {
        _playerShip.changeHealthEvent.OnChangeHealth -= ChangeHealthEvent_UpdateUIPlayerHealth;
    }

    private void Awake()
    {
        _playerShip = GameManager.Instance.GetPlayerShip().GetComponent<Ship>();
        _health = _playerShip.health;
        _currentHealth = _health.CurrentHealth;       
    }

    private void Start()
    {
        _currentHealth = _health.CurrentHealth;
        InitHealthUI();
    }

    private void InitHealthUI()
    {
        string healthText;
        healthText = "Current Health: " + _currentHealth;
        healthTMP.text = healthText;
    }

    private void ChangeHealthEvent_UpdateUIPlayerHealth(ChangeHealthEventArgs changeHealthEventArgs)
    {
        UpdateUIPlayerHealth(0);
    }

    private void UpdateUIPlayerHealth (int damage)
    {
        _currentHealth = _health.CurrentHealth;
        InitHealthUI();
    }

}
