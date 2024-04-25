using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogcController : MonoBehaviour
{
    public float moveSpeed = 2f; // ������ �̵� �ӵ�
    public float movementRange = 3f; // �̵� ������ ����
    private float leftBound; // ���� ��� ����
    private float rightBound; // ���� ��� ����
    private float currentSpeed; // ���� �̵� �ӵ�
    private bool movingRight = true; // ������ �̵� ����

    void Start()
    {
        currentSpeed = moveSpeed; // ������ �� ���� �̵� �ӵ��� �ʱ�ȭ
        leftBound = transform.position.x - movementRange; // ���� ��� ���� ����
        rightBound = transform.position.x + movementRange; // ���� ��� ���� ����
    }

    void Update()
    {
        // ���͸� �¿�� �̵���Ŵ
        if (movingRight) // ������ �������� ���
        {
            transform.Translate(Vector2.right * currentSpeed * Time.deltaTime); // ���������� �̵�
        }
        else // ������ ������ ���
        {
            transform.Translate(Vector2.left * currentSpeed * Time.deltaTime); // �������� �̵�
        }
        // ��踦 �Ѿ�� ������ ��ȯ��
        if (transform.position.x >= rightBound) // ���� ��踦 �Ѿ ���
        {
            movingRight = false; // �̵� ������ �������� ����
            FlipSprite(); // ��������Ʈ�� �������� �¿� ������ ǥ��
        }
        else if (transform.position.x <= leftBound) // ���� ��踦 �Ѿ ���
        {
            movingRight = true; // �̵� ������ ���������� ����
            FlipSprite(); // ��������Ʈ�� �������� �¿� ������ ǥ��
        }
    }

    void FlipSprite() // ��������Ʈ�� �������� �¿� ������ ǥ��
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        // ��������Ʈ�� ���� �������� �������� �¿� ������ ��ȯ
    }

}