using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int _startHealth;
    public int _currentHealth;
    private Ship _ship;

    private void OnEnable()
    {
        _ship.changeHealthEvent.OnChangeHealth += ChangeHealthEvent_TestChangeHealth;
    }

    private void OnDisable()
    {
        _ship.changeHealthEvent.OnChangeHealth -= ChangeHealthEvent_TestChangeHealth;
    }

    public int CurrentHealth
    {
        get { return _currentHealth; }
    }

    private void Awake()
    {
        _ship = GetComponent<Ship>();
        InitHealth();
    }

    private void InitHealth()
    {
        _startHealth = _ship._currentShipDetails.startHealth;
        _currentHealth = _startHealth;
    }

    private void ChangeHealthEvent_TestChangeHealth(ChangeHealthEventArgs changeHealthEventArgs)
    {
        TakeDamage(changeHealthEventArgs.damage);
    }

    private void TakeDamage(int damage)
    {
        _currentHealth -= damage;
    }


}
