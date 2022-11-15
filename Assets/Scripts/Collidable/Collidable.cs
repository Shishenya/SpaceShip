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

        // Столкновения противника
        if (gameObject.GetComponent<Enemy>()!=null)
        {
            SwitchCollisionEnemy(collision);
        }

        if (gameObject.GetComponent<Player>()!=null)
        {
            SwitchCollisionPlayer(collision);
        }

        // Попадание снаряда
        if (collision.gameObject.GetComponent<Ammo>()!=null && gameObject.GetComponent<Health>()!=null)
        {
            collision.gameObject.SetActive(false);
            int damage = collision.gameObject.GetComponent<Ammo>().GetRandomDamage(); // получаем урон

            #region BAG!
            // Какой то странный баг, связанный с коллизией. По противнику урон проходит почему то
            // Два раза. Пока поставлю загрулшку на это
            if (gameObject.GetComponent<Enemy>() != null) damage = damage / 2;
            #endregion

            _ship.changeHealthEvent.CallChangeHealthEvent(damage);            
            Debug.Log("Попадания снаряда c уроном " + damage + " по объейкту " + _ship.gameObject.name);
        }

    }



    /// <summary>
    /// Ответы на столкновения врагов
    /// </summary>
    private void SwitchCollisionEnemy(Collision2D collision)
    {
        // Debug.Log("События столкновения врага");

        // Столкнулся сл стеной
        if (collision.gameObject.GetComponent<BorderCollider>()!=null)
        {
            // Debug.Log("Враг вышел за пределы экрана");
            _ship.deathEvent.CallOnDeathEvent(); // Событие смерти корабля при его выходе за пределы экрана
            GameManager.Instance.GameLost(); // поражение игрока
        }

        // Столкнулся с игроком
        if (collision.gameObject == GameManager.Instance.GetPlayerShip())
        {
            GameManager.Instance.GetPlayerShip().GetComponent<Ship>().changeHealthEvent.CallChangeHealthEvent(1); // наносим игроку урон
            _ship.changeHealthEvent.CallChangeHealthEvent(2); // наносим урон кораблю
            _ship.deathEvent.CallOnDeathEvent(); // Событие смерти корабля при столкновении
        }


    }

    /// <summary>
    /// Реакци на столкновения игрока
    /// </summary>
    private void SwitchCollisionPlayer(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Bonus>()!=null)
        {
            // прибавляем здоровье
            _ship.changeHealthEvent.CallChangeHealthEvent(-collision.gameObject.GetComponent<Bonus>().bonusDetails.addHealthBonus);
            collision.gameObject.SetActive(false); // деактивируем бонус
        }
    }

}
