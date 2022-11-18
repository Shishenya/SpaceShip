using UnityEngine;

[RequireComponent(typeof(DeathEvent))]
[RequireComponent(typeof(Health))]
[DisallowMultipleComponent]
public class Meteor : MonoBehaviour, IDeadable
{

    [SerializeField] private GameObject _deathEffectGO;
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
    public void DeathEvent_OnDeath()
    {
        Death();
    }

    /// <summary>
    /// Уничтожение метеорита
    /// </summary>
    public void Death()
    {
        DeathEffect();
        _health.GetStartHealth(); // устанавлиаем значение ХП на стартовое
        gameObject.SetActive(false); // отключаем метеорит
    }

    /// <summary>
    /// Эффект смерти
    /// </summary>
    private void DeathEffect()
    {
        GameObject effect = PoolManager.Instance.GetFromThePool(_deathEffectGO);
        effect.transform.position = transform.position;
        effect.SetActive(true);
    }
}
