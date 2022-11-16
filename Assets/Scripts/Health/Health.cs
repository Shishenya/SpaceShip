using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ChangeHealthEvent))]
[RequireComponent(typeof(DeathEvent))]
[DisallowMultipleComponent]
public class Health : MonoBehaviour
{
    private int _startHealth;
    public int _currentHealth;

    [HideInInspector] public ChangeHealthEvent changeHealthEvent;
    private DeathEvent _deathEvent;

    [SerializeField] private HealthDetailsSO healthDetails;

    private void OnEnable()
    {
        changeHealthEvent.OnChangeHealth += ChangeHealthEvent_ChangeHealth;
        _deathEvent.OnDeath += SoundEffectDeath;
    }

    private void OnDisable()
    {
        changeHealthEvent.OnChangeHealth -= ChangeHealthEvent_ChangeHealth;
        _deathEvent.OnDeath -= SoundEffectDeath;
    }

    public int CurrentHealth
    {
        get { return _currentHealth; }
    }

    public int StartHealth
    {
        get { return _startHealth;  }
    }

    private void Awake()
    {
        changeHealthEvent = GetComponent<ChangeHealthEvent>();
        _deathEvent = GetComponent<DeathEvent>();
        GetStartHealth();
    }

    /// <summary>
    /// ��������� ���������� �������� ��������
    /// </summary>
    public void GetStartHealth()
    {

        if (healthDetails!=null)
        {
            _startHealth = healthDetails.startHealth;
        }
        else
        {
            _startHealth = 1;
        }

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
        if (_currentHealth <= 0) _deathEvent.CallOnDeathEvent();

        // ������������ �������� - �� ���� �����������
        if (_currentHealth > _startHealth) _currentHealth = _startHealth;
    }

    /// <summary>
    /// ���� �����������
    /// </summary>
    private void SoundEffectDeath(DeathEventArgs deathEventArgs)
    {
        if (healthDetails.soundDestroy!=null)
        {
            SoundEffectManager.Instance.PlaySoundEffect(healthDetails.soundDestroy);
        }
    }


}
