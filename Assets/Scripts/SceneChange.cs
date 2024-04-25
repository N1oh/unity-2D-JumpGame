using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    // 게임 씬으로 전환하는 함수
    public void LoadScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    // 시작 씬으로 전환하는 함수
    public void LoadScene1()
    {
        // 코인 수를 초기화
        Score.coinAmount = 0;
        // 시작 씬으로 전환
        SceneManager.LoadScene("StartScene");
    }
}
