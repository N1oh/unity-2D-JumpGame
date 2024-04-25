using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartController : MonoBehaviour
{
    private GameDirector gameDirector; // GameDirector 스크립트를 참조할 변수

    void Start()
    {
        // "GameDirector"라는 이름의 게임 오브젝트를 찾아서 GameDirector 컴포넌트를 가져와 gameDirector 변수에 할당
        gameDirector = GameObject.Find("GameDirector").GetComponent<GameDirector>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 만약 충돌한 게임 오브젝트의 태그가 "Player"인 경우
        if (other.gameObject.tag == "Player")
        {
            // gameDirector의 IncreaseHp 메서드를 호출하여 체력을 증가시킴
            gameDirector.IncreaseHp();

            // 현재 게임 오브젝트를 삭제
            Destroy(gameObject);
        }
    }
}