using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[DisallowMultipleComponent]
public class Ammo : MonoBehaviour
{
    private Vector3 _directionFire;
    private float _speedAmmo;
    private int _minDamage;
    private int _maxDamage;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Инициализация снаряда
    /// </summary>
    public void InitAmmo(Vector3 direction, float speed, int minDamage, int maxDamage)
    {
        _directionFire = direction;
        _speedAmmo = speed;
        _minDamage = minDamage;
        _maxDamage = maxDamage;
    }

    private void Update()
    {
        MoveAmmo(); // двигаем снаряд
    }

    /// <summary>
    /// Движение снаряда
    /// </summary>
    private void MoveAmmo()
    {
        _rigidbody2D.velocity = _directionFire * _speedAmmo; // запускам его 
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    gameObject.SetActive(false);
    //}

    /// <summary>
    /// Возвращает случайны урон
    /// </summary>
    /// <returns></returns>
    public int GetRandomDamage()
    {
        return Random.Range(_minDamage, _maxDamage + 1);
    }

}
