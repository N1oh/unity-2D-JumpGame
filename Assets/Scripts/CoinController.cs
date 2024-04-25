using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider) // 오브젝트와 충돌하면 호출되는 함수
    {
        // 만약 충돌한 게임 오브젝트의 태그가 "Player"인 경우
        if (collider.gameObject.CompareTag("Player"))
        {
            // 현재 게임 오브젝트를 삭제
            Destroy(gameObject);

            // 점수에 100을 추가
            Score.coinAmount += 100;
        }
    }
}