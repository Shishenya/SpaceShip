using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DeathEvent))]
[RequireComponent(typeof(Health))]
[DisallowMultipleComponent]
public class Meteor : MonoBehaviour, IDeadable
{

    [SerializeField] private GameObject deathEffectGO;
    private DeathEvent _deathEvent;
    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _deathEvent = GetComponent<DeathEvent>();
    }

    private void OnEnable()
    {
        _deathEvent.OnDeath += DeathEvent_OnDeath;
    }

    private void OnDisable()
    {
        _deathEvent.OnDeath -= DeathEvent_OnDeath;
    }

    /// <summary>
    /// Handle
    /// </summary>
    public void DeathEvent_OnDeath(DeathEventArgs deathEventArgs)
    {
        Death();
    }

    /// <summary>
    /// ����������� ���������
    /// </summary>
    public void Death()
    {
        DeathEffect();
        _health.GetStartHealth(); // ������������ �������� �� �� ���������
        gameObject.SetActive(false); // ��������� ��������
    }

    private void DeathEffect()
    {
        GameObject effect = PoolManager.Instance.GetFromThePool(deathEffectGO);
        effect.transform.position = transform.position;
        effect.SetActive(true);
    }
}
