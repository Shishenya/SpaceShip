using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Required Component
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PolygonCollider2D))]

[RequireComponent(typeof(MovementEvent))]
[RequireComponent(typeof(Movement))]

[RequireComponent(typeof(FireWeaponEvent))]
[RequireComponent(typeof(FireWeapon))]
[RequireComponent(typeof(Weapon))]

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(ChangeHealthEvent))]
#endregion
[DisallowMultipleComponent]
public class Ship : MonoBehaviour
{
    [Tooltip("Game Object Weapon Shoot Position")]
    [SerializeField] private GameObject _weaponShootPosition;
    [Tooltip("Game Object Weapon Effect Shoot Position")]
    [SerializeField] private GameObject _weaponEffectShootPosition;

    public ShipDetailsSO _currentShipDetails = null;
    private Rigidbody2D _rigidbody2D = null;

    // Event Components
    [HideInInspector] public MovementEvent movementEvent;
    [HideInInspector] public FireWeaponEvent fireWeaponEvent;
    [HideInInspector] public Weapon weapon;
    [HideInInspector] public Health health;
    [HideInInspector] public ChangeHealthEvent changeHealthEvent;

    // Weapon Details test
    public WeaponDetailsSO weaponDetails;

    private void Awake()
    {
        // Load components
        _rigidbody2D = GetComponent<Rigidbody2D>();
        movementEvent = GetComponent<MovementEvent>();
        fireWeaponEvent = GetComponent<FireWeaponEvent>();
        weapon = GetComponent<Weapon>();
        health = GetComponent<Health>();
        changeHealthEvent = GetComponent<ChangeHealthEvent>();
    }

    public void InitShip(ShipDetailsSO shipDetails)
    {
        _currentShipDetails = shipDetails;
    }

}
