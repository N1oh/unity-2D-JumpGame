using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogcController : MonoBehaviour
{
    public float moveSpeed = 2f; // 몬스터의 이동 속도
    public float movementRange = 3f; // 이동 가능한 범위
    private float leftBound; // 좌측 경계 지점
    private float rightBound; // 우측 경계 지점
    private float currentSpeed; // 현재 이동 속도
    private bool movingRight = true; // 몬스터의 이동 방향

    void Start()
    {
        currentSpeed = moveSpeed; // 시작할 때 현재 이동 속도를 초기화
        leftBound = transform.position.x - movementRange; // 좌측 경계 지점 설정
        rightBound = transform.position.x + movementRange; // 우측 경계 지점 설정
    }

    void Update()
    {
        // 몬스터를 좌우로 이동시킴
        if (movingRight) // 방향이 오른쪽인 경우
        {
            transform.Translate(Vector2.right * currentSpeed * Time.deltaTime); // 오른쪽으로 이동
        }
        else // 방향이 왼쪽인 경우
        {
            transform.Translate(Vector2.left * currentSpeed * Time.deltaTime); // 왼쪽으로 이동
        }
        // 경계를 넘어가면 방향을 전환함
        if (transform.position.x >= rightBound) // 우측 경계를 넘어간 경우
        {
            movingRight = false; // 이동 방향을 왼쪽으로 변경
            FlipSprite(); // 스프라이트를 반전시켜 좌우 방향을 표현
        }
        else if (transform.position.x <= leftBound) // 좌측 경계를 넘어간 경우
        {
            movingRight = true; // 이동 방향을 오른쪽으로 변경
            FlipSprite(); // 스프라이트를 반전시켜 좌우 방향을 표현
        }
    }

    void FlipSprite() // 스프라이트를 반전시켜 좌우 방향을 표현
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        // 스프라이트의 로컬 스케일을 반전시켜 좌우 방향을 전환
    }

}