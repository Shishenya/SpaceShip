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
            GameManager.Instance.GetPlayerShip().GetComponent<Ship>().changeHealthEvent.CallChangeHealthEvent(5); // ������� ������ ����
            _ship.changeHealthEvent.CallChangeHealthEvent(5); // ������� ���� �������
            _ship.deathEvent.CallOnDeathEvent(); // ������� ������ ������� ��� ������������
        }


    }



}
