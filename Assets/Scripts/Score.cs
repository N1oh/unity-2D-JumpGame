using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text text;              // �ؽ�Ʈ ������Ʈ�� �����ϱ� ���� ����
    public static int coinAmount;    // ���� ������ �����ϱ� ���� ����

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();    // �ڽ��� ���� ������Ʈ���� Text ������Ʈ�� ������
    }

    // Update is called once per frame
    void Update()
    {
        text.text = coinAmount.ToString();    // ���� ������ ���ڿ��� ��ȯ�Ͽ� �ؽ�Ʈ�� ǥ��
    }
}