using UnityEngine;

[RequireComponent(typeof(ChangeHealthEvent))]
[RequireComponent(typeof(DeathEvent))]
[DisallowMultipleComponent]
public class Health : MonoBehaviour
{
    private int _startHealth;
    private int _currentHealth;
    private DeathEvent _deathEvent;

    [HideInInspector] public ChangeHealthEvent changeHealthEvent;
    [SerializeField] private HealthDetailsSO _healthDetails;

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
    /// Установка стартового значения здоровья
    /// </summary>
    public void GetStartHealth()
    {

        if (_healthDetails!=null)
        {
            _startHealth = _healthDetails.startHealth;
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
    /// Получение дамага кораблем
    /// </summary>
    private void TakeDamage(int damage)
    {
        _currentHealth -= damage; // Наносим дамаг

        // Если здоровье меньше нуля, то смерть
        if (_currentHealth <= 0) _deathEvent.CallOnDeathEvent();

        // Максимальное здоровье - не выше старотового
        if (_currentHealth > _startHealth) _currentHealth = _startHealth;
    }

    /// <summary>
    /// Звук уничтожения
    /// </summary>
    private void SoundEffectDeath()
    {
        if (_healthDetails.soundDestroy!=null)
        {
            SoundEffectManager.Instance.PlaySoundEffect(_healthDetails.soundDestroy);
        }
    }


}
