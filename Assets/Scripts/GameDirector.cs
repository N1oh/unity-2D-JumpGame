using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    GameObject gPlayer = null;  // 플레이어 GameObject 변수
    GameObject hpGauge = null;  // 체력 게이지 GameObject 변수
    GameObject gameOver = null; // 게임 오버 UI GameObject 변수
    Button ResetButton = null;  // 게임 재시작 버튼(Button) 변수

    void Start()
    {
        // "Pink"라는 이름의 게임 오브젝트를 찾아 gPlayer 변수에 할당
        gPlayer = GameObject.Find("Pink");

        // "hpGauge"라는 이름의 게임 오브젝트를 찾아 hpGauge 변수에 할당
        hpGauge = GameObject.Find("hpGauge");

        // "GameOver"라는 이름의 게임 오브젝트를 찾아 gameOver 변수에 할당
        gameOver = GameObject.Find("GameOver");

        // 시작 시 게임 오버 UI를 비활성화
        gameOver.SetActive(false);

        // "ResetButton"이라는 이름의 게임 오브젝트를 찾아 ResetButton 변수에 할당하고, 해당 게임 오브젝트의 Button 컴포넌트를 가져옴
        ResetButton = GameObject.Find("ResetButton").GetComponent<Button>();

        // "게임 재시작" 버튼 클릭 시 RestartGame 메소드 실행
        ResetButton.onClick.AddListener(RestartGame);

        // 게임 일시 정지 해제
        Time.timeScale = 1;

        // "게임 재시작" 버튼을 비활성화
        ResetButton.gameObject.SetActive(false);
    }

    // DecreaseHp 메서드는 체력을 감소시키는 역할을 함
    public void DecreaseHp()
    {
        // hpGauge의 Image 컴포넌트의 fillAmount를 0.1씩 감소시킴
        hpGauge.GetComponent<Image>().fillAmount -= 0.1f;

        // hpGauge의 fillAmount가 0 이하일 경우 게임 오버 처리
        if (hpGauge.GetComponent<Image>().fillAmount <= 0)
        {
            GameOver();
        }
    }

    // IncreaseHp 메서드는 체력을 증가시키는 역할을 함
    public void IncreaseHp()
    {
        // hpGauge의 Image 컴포넌트의 fillAmount를 0.1씩 증가시킴
        hpGauge.GetComponent<Image>().fillAmount += 0.1f;
    }

    // GameOver 메서드는 게임 오버 상태를 처리함
    void GameOver()
    {
        // 게임 일시 정지
        Time.timeScale = 0;

        // 게임 오버 UI 활성화
        gameOver.SetActive(true);

        // 게임 오버 텍스트 설정
        gameOver.GetComponent<Text>().text = "Game Over";

        // "게임 재시작" 버튼 활성화
        ResetButton.gameObject.SetActive(true);
    }

    // RestartGame 메서드는 게임을 재시작함
    void RestartGame()
    {
        // 현재 씬을 다시 로드하여 게임을 재시작
        Score.coinAmount = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}