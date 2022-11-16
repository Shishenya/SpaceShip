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
        if (collision.gameObject.GetComponent<Health>()!=null)
        {
            Health health = collision.gameObject.GetComponent<Health>();
            int damage = _ammo.GetRandomDamage();
            health.changeHealthEvent.CallChangeHealthEvent(damage);
            gameObject.SetActive(false);
        }

    }
}
