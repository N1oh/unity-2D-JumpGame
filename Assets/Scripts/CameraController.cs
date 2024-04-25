using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // �÷��̾� ������Ʈ�� ã�� ���ؼ� ��� ���� ����
    GameObject m_Player = null;
    // �÷��̾  �̵��� ������ ī�޶� ����ٴϵ��� �÷��̾� X,Y��ǥ ���� ����
    [SerializeField]
    Vector3 vPlayerPositon = Vector3.zero;

    void Start()
    {
        // �÷��̾� ������Ʈ�� ã�Ƽ� ��� ������ ���� 
        m_Player = GameObject.Find("Pink");
    }

    void Update()
    {
        // �÷��̾��� ��ġ�� ������
        vPlayerPositon = m_Player.transform.position;
        // ī�޶��� ��ġ�� �÷��̾��� X�� Y ��ġ�� ���߰�, ī�޶��� ���� Z ��ġ�� ����
        transform.position = new Vector3(vPlayerPositon.x, vPlayerPositon.y, transform.position.z);
    }
}