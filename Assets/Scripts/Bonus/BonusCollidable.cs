using UnityEngine;

public class BonusCollidable : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            // прибавляем здоровье
            GameManager.Instance.GetPlayerShip().GetComponent<Ship>().changeHealthEvent.CallChangeHealthEvent(-gameObject.GetComponent<Bonus>().bonusDetails.addHealthBonus);
            gameObject.SetActive(false); // деактивируем бонус
        }
    }
}
