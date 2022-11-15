using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int _startHealth;
    public int _currentHealth;
    private Ship _ship;

    private ChangeHealthEvent _changeHealthEvent;

    private void OnEnable()
    {
        _ship.changeHealthEvent.OnChangeHealth += ChangeHealthEvent_ChangeHealth;
    }

    private void OnDisable()
    {
        _ship.changeHealthEvent.OnChangeHealth -= ChangeHealthEvent_ChangeHealth;
    }

    public int CurrentHealth
    {
        get { return _currentHealth; }
    }

    private void Awake()
    {
        _ship = GetComponent<Ship>();
        _changeHealthEvent = GetComponent<ChangeHealthEvent>();
        GetStartHealth();
    }

    /// <summary>
    /// Установка стартового значения здоровья
    /// </summary>
    public void GetStartHealth()
    {
        _startHealth = _ship._currentShipDetails.startHealth;
        _currentHealth = _startHealth;
    }

    private void ChangeHealthEvent_ChangeHealth(ChangeHealthEventArgs changeHealthEventArgs)
    {
        TakeDamage(changeHealthEventArgs.damage);
    }

    /// <summary>
    /// Получение дамага кораблем
    /// </summary>
    private void TakeDamage(int damage)
    {
        _currentHealth -= damage; // Наносим дамаг

        // Если здоровье меньше нуля, то смерть
        if (_currentHealth <= 0) _ship.deathEvent.CallOnDeathEvent();

        // Максимальное здоровье - не выше старотового
        if (_currentHealth > _startHealth) _currentHealth = _startHealth;
    }


}
