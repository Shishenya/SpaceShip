using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidable : MonoBehaviour
{

    private Ship _ship;

    private void Awake()
    {
        _ship = GetComponent<Ship>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Debug.Log("Столкнулся");

        // Столкновения противника
        if (gameObject.GetComponent<EnemyMoveAI>()!=null)
        {
            SwitchCollisionEnemy(collision);
        }

        // Попадание снаряда
        if (collision.gameObject.GetComponent<Ammo>()!=null && gameObject.GetComponent<Health>()!=null)
        {
            Debug.Log("Попадания снаряда");
            int damage = collision.gameObject.GetComponent<Ammo>().GetRandomDamage(); // получаем урон
            _ship.changeHealthEvent.CallChangeHealthEvent(damage);
            collision.gameObject.SetActive(false);
        }

    }



    /// <summary>
    /// Ответы на столкновения врагов
    /// </summary>
    /// <param name="collision"></param>
    private void SwitchCollisionEnemy(Collision2D collision)
    {
        Debug.Log("События столкновения врага");

        // Столкнулся сл стеной
        if (collision.gameObject.GetComponent<BorderCollider>()!=null)
        {
            // Debug.Log("Враг вышел за пределы экрана");
            _ship.deathEvent.CallOnDeathEvent(); // Событие смерти корабля при его выходе за пределы экрана
        }

        // Столкнулся с игроком
        if (collision.gameObject == GameManager.Instance.GetPlayerShip())
        {
            GameManager.Instance.GetPlayerShip().GetComponent<Ship>().changeHealthEvent.CallChangeHealthEvent(1); // наносим игроку урон
            _ship.changeHealthEvent.CallChangeHealthEvent(2); // наносим урон кораблю
            _ship.deathEvent.CallOnDeathEvent(); // Событие смерти корабля при столкновении
        }


    }
}
