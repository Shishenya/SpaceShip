using UnityEngine;

#region Required Component
[RequireComponent(typeof(Rigidbody2D))]

[RequireComponent(typeof(MovementEvent))]
[RequireComponent(typeof(Movement))]

[RequireComponent(typeof(FireWeaponEvent))]
[RequireComponent(typeof(FireWeapon))]
[RequireComponent(typeof(Weapon))]

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(ChangeHealthEvent))]
[RequireComponent(typeof(DeathEvent))]
#endregion
[DisallowMultipleComponent]
public class Ship : MonoBehaviour
{
    [Tooltip("Game Object Weapon Shoot Position")]
    [SerializeField] private GameObject _weaponShootPosition;
    [Tooltip("Game Object Weapon Effect Shoot Position")]
    [SerializeField] private GameObject _weaponEffectShootPosition;

    public ShipDetailsSO _currentShipDetails = null;

    // Event Components
    [HideInInspector] public MovementEvent movementEvent;
    [HideInInspector] public FireWeaponEvent fireWeaponEvent;
    [HideInInspector] public Weapon weapon;
    [HideInInspector] public Health health;
    [HideInInspector] public ChangeHealthEvent changeHealthEvent;
    [HideInInspector] public DeathEvent deathEvent;

    // Weapon Details test
    public WeaponDetailsSO weaponDetails;

    private void Awake()
    {
        // Load components
        movementEvent = GetComponent<MovementEvent>();
        fireWeaponEvent = GetComponent<FireWeaponEvent>();
        weapon = GetComponent<Weapon>();
        health = GetComponent<Health>();
        changeHealthEvent = GetComponent<ChangeHealthEvent>();
        deathEvent = GetComponent<DeathEvent>();
    }

    public void InitShip(ShipDetailsSO shipDetails)
    {
        _currentShipDetails = shipDetails;
    }

}
