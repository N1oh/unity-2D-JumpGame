using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartController : MonoBehaviour
{
    private GameDirector gameDirector; // GameDirector ��ũ��Ʈ�� ������ ����

    void Start()
    {
        // "GameDirector"��� �̸��� ���� ������Ʈ�� ã�Ƽ� GameDirector ������Ʈ�� ������ gameDirector ������ �Ҵ�
        gameDirector = GameObject.Find("GameDirector").GetComponent<GameDirector>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // ���� �浹�� ���� ������Ʈ�� �±װ� "Player"�� ���
        if (other.gameObject.tag == "Player")
        {
            // gameDirector�� IncreaseHp �޼��带 ȣ���Ͽ� ü���� ������Ŵ
            gameDirector.IncreaseHp();

            // ���� ���� ������Ʈ�� ����
            Destroy(gameObject);
        }
    }
}