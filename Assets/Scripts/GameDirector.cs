using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    GameObject gPlayer = null;  // �÷��̾� GameObject ����
    GameObject hpGauge = null;  // ü�� ������ GameObject ����
    GameObject gameOver = null; // ���� ���� UI GameObject ����
    Button ResetButton = null;  // ���� ����� ��ư(Button) ����

    void Start()
    {
        // "Pink"��� �̸��� ���� ������Ʈ�� ã�� gPlayer ������ �Ҵ�
        gPlayer = GameObject.Find("Pink");

        // "hpGauge"��� �̸��� ���� ������Ʈ�� ã�� hpGauge ������ �Ҵ�
        hpGauge = GameObject.Find("hpGauge");

        // "GameOver"��� �̸��� ���� ������Ʈ�� ã�� gameOver ������ �Ҵ�
        gameOver = GameObject.Find("GameOver");

        // ���� �� ���� ���� UI�� ��Ȱ��ȭ
        gameOver.SetActive(false);

        // "ResetButton"�̶�� �̸��� ���� ������Ʈ�� ã�� ResetButton ������ �Ҵ��ϰ�, �ش� ���� ������Ʈ�� Button ������Ʈ�� ������
        ResetButton = GameObject.Find("ResetButton").GetComponent<Button>();

        // "���� �����" ��ư Ŭ�� �� RestartGame �޼ҵ� ����
        ResetButton.onClick.AddListener(RestartGame);

        // ���� �Ͻ� ���� ����
        Time.timeScale = 1;

        // "���� �����" ��ư�� ��Ȱ��ȭ
        ResetButton.gameObject.SetActive(false);
    }

    // DecreaseHp �޼���� ü���� ���ҽ�Ű�� ������ ��
    public void DecreaseHp()
    {
        // hpGauge�� Image ������Ʈ�� fillAmount�� 0.1�� ���ҽ�Ŵ
        hpGauge.GetComponent<Image>().fillAmount -= 0.1f;

        // hpGauge�� fillAmount�� 0 ������ ��� ���� ���� ó��
        if (hpGauge.GetComponent<Image>().fillAmount <= 0)
        {
            GameOver();
        }
    }

    // IncreaseHp �޼���� ü���� ������Ű�� ������ ��
    public void IncreaseHp()
    {
        // hpGauge�� Image ������Ʈ�� fillAmount�� 0.1�� ������Ŵ
        hpGauge.GetComponent<Image>().fillAmount += 0.1f;
    }

    // GameOver �޼���� ���� ���� ���¸� ó����
    void GameOver()
    {
        // ���� �Ͻ� ����
        Time.timeScale = 0;

        // ���� ���� UI Ȱ��ȭ
        gameOver.SetActive(true);

        // ���� ���� �ؽ�Ʈ ����
        gameOver.GetComponent<Text>().text = "Game Over";

        // "���� �����" ��ư Ȱ��ȭ
        ResetButton.gameObject.SetActive(true);
    }

    // RestartGame �޼���� ������ �������
    void RestartGame()
    {
        // ���� ���� �ٽ� �ε��Ͽ� ������ �����
        Score.coinAmount = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}