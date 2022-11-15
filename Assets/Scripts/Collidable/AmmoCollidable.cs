using UnityEngine;

public class AmmoCollidable : MonoBehaviour
{
    private Ammo _ammo;
    private void Awake()
    {
        _ammo = GetComponent<Ammo>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Ship>()!=null)
        {
            Ship ship = collision.gameObject.GetComponent<Ship>();
            int damage = _ammo.GetRandomDamage();
            ship.changeHealthEvent.CallChangeHealthEvent(damage);
            gameObject.SetActive(false);
        }
    }
}
