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

        Debug.Log("����������");

        // ������������ ����������
        if (gameObject.GetComponent<EnemyMoveAI>()!=null)
        {
            SwitchCollisionEnemy(collision);
        }

        // ��������� �������
        if (collision.gameObject.GetComponent<Ammo>()!=null && gameObject.GetComponent<Health>()!=null)
        {
            Debug.Log("��������� �������");
            int damage = collision.gameObject.GetComponent<Ammo>().GetRandomDamage(); // �������� ����
            _ship.changeHealthEvent.CallChangeHealthEvent(damage);
            collision.gameObject.SetActive(false);
        }

    }



    /// <summary>
    /// ������ �� ������������ ������
    /// </summary>
    /// <param name="collision"></param>
    private void SwitchCollisionEnemy(Collision2D collision)
    {
        Debug.Log("������� ������������ �����");

        // ���������� �� ������
        if (collision.gameObject.GetComponent<BorderCollider>()!=null)
        {
            Debug.Log("���� ����� �� ������� ������");
            _ship.gameObject.SetActive(false); // ������������
        }

        // ���������� � �������
        if (collision.gameObject == GameManager.Instance.GetPlayerShip())
        {
            Debug.Log("���������� � �������� ������");
            GameManager.Instance.GetPlayerShip().GetComponent<Ship>().changeHealthEvent.CallChangeHealthEvent(1); // ������� ������ ����
            _ship.changeHealthEvent.CallChangeHealthEvent(2); // ������� ���� �������
            _ship.gameObject.SetActive(false); // ������������
        }


    }
}
