using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    // ���� ������ ��ȯ�ϴ� �Լ�
    public void LoadScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    // ���� ������ ��ȯ�ϴ� �Լ�
    public void LoadScene1()
    {
        // ���� ���� �ʱ�ȭ
        Score.coinAmount = 0;
        // ���� ������ ��ȯ
        SceneManager.LoadScene("StartScene");
    }
}
