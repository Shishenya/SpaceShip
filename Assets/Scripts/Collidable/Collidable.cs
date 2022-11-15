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

        // ������������ ����������
        if (gameObject.GetComponent<Enemy>()!=null)
        {
            SwitchCollisionEnemy(collision);
        }

        if (gameObject.GetComponent<Player>()!=null)
        {
            SwitchCollisionPlayer(collision);
        }

        // ��������� �������
        if (collision.gameObject.GetComponent<Ammo>()!=null && gameObject.GetComponent<Health>()!=null)
        {
            collision.gameObject.SetActive(false);
            int damage = collision.gameObject.GetComponent<Ammo>().GetRandomDamage(); // �������� ����

            #region BAG!
            // ����� �� �������� ���, ��������� � ���������. �� ���������� ���� �������� ������ ��
            // ��� ����. ���� �������� ��������� �� ���
            if (gameObject.GetComponent<Enemy>() != null) damage = damage / 2;
            #endregion

            _ship.changeHealthEvent.CallChangeHealthEvent(damage);            
            Debug.Log("��������� ������� c ������ " + damage + " �� �������� " + _ship.gameObject.name);
        }

    }



    /// <summary>
    /// ������ �� ������������ ������
    /// </summary>
    private void SwitchCollisionEnemy(Collision2D collision)
    {
        // Debug.Log("������� ������������ �����");

        // ���������� �� ������
        if (collision.gameObject.GetComponent<BorderCollider>()!=null)
        {
            // Debug.Log("���� ����� �� ������� ������");
            _ship.deathEvent.CallOnDeathEvent(); // ������� ������ ������� ��� ��� ������ �� ������� ������
            GameManager.Instance.GameLost(); // ��������� ������
        }

        // ���������� � �������
        if (collision.gameObject == GameManager.Instance.GetPlayerShip())
        {
            GameManager.Instance.GetPlayerShip().GetComponent<Ship>().changeHealthEvent.CallChangeHealthEvent(1); // ������� ������ ����
            _ship.changeHealthEvent.CallChangeHealthEvent(2); // ������� ���� �������
            _ship.deathEvent.CallOnDeathEvent(); // ������� ������ ������� ��� ������������
        }


    }

    /// <summary>
    /// ������ �� ������������ ������
    /// </summary>
    private void SwitchCollisionPlayer(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Bonus>()!=null)
        {
            // ���������� ��������
            _ship.changeHealthEvent.CallChangeHealthEvent(-collision.gameObject.GetComponent<Bonus>().bonusDetails.addHealthBonus);
            collision.gameObject.SetActive(false); // ������������ �����
        }
    }

}
