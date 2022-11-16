using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorCollidable : MonoBehaviour
{

    private int _damage = 3;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            Ship ship = collision.gameObject.GetComponent<Ship>();
            ship.changeHealthEvent.CallChangeHealthEvent(_damage);
            gameObject.SetActive(false);
        }

        if (collision.gameObject.GetComponent<BorderCollider>() != null)
        {
            gameObject.SetActive(false);
        }

    }
}
