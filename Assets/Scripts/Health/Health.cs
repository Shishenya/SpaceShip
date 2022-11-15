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
        GetStartHealth();
    }

    /// <summary>
    /// ��������� ���������� �������� ��������
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
    /// ��������� ������ ��������
    /// </summary>
    private void TakeDamage(int damage)
    {
        _currentHealth -= damage; // ������� �����

        // ���� �������� ������ ����, �� ������
        if (_currentHealth <= 0) _ship.deathEvent.CallOnDeathEvent();
    }


}
