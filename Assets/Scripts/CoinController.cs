using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider) // ������Ʈ�� �浹�ϸ� ȣ��Ǵ� �Լ�
    {
        // ���� �浹�� ���� ������Ʈ�� �±װ� "Player"�� ���
        if (collider.gameObject.CompareTag("Player"))
        {
            // ���� ���� ������Ʈ�� ����
            Destroy(gameObject);

            // ������ 100�� �߰�
            Score.coinAmount += 100;
        }
    }
}