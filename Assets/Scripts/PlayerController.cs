using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    AudioSource m_aud; // AudioSource 변수를 선언합니다.
    Rigidbody2D m_rigid2D;  // Rigidbody2D 변수를 선언합니다.
    Animator m_animator;  //Animator 변수를 선언합니다.
    SpriteRenderer m_spriteRenderer; // SpriteRenderer 변수를 선언합니다.

    [SerializeField]
    float fjumpForce = 680.0f; // 플레이어에게 가할 점프 힘

    [SerializeField]
    float fwalkForce = 30.0f; // 플레이어 이동에 가할 힘

    [SerializeField]
    float fmaxWalkSpeed = 2.0f; // 이동 속도의 최대값

    public AudioClip jump; // 점프 사운드
    public AudioClip hit; // 충돌 사운드
    public AudioClip coin; // 코인 획득 사운드
    public AudioClip heal; // 회복 아이템 획득 사운드
    public AudioClip clear; // 클리어 사운드

    void Start()
    {
        m_rigid2D = GetComponent<Rigidbody2D>(); // Rigidbody2D 컴포넌트 가져오기
        m_animator = GetComponent<Animator>(); // Animator 컴포넌트 가져오기
        m_spriteRenderer = GetComponent<SpriteRenderer>(); // SpriteRenderer 컴포넌트 가져오기
        m_aud = GetComponent<AudioSource>(); // AudioSource 컴포넌트 가져오기
    }

    void Update()
    {
        // 점프 동작을 처리합니다. 플레이어의 수직 속도가 0인 경우에만 실행됩니다.
        if (Input.GetKeyDown(KeyCode.Space) && m_rigid2D.velocity.y == 0)
        {
            // fjumpForce 만큼의 힘을 위쪽 방향으로 가해줍니다.
            m_rigid2D.AddForce(transform.up * fjumpForce);
            // jump 오디오 클립을 설정하고 재생합니다.
            m_aud.clip = jump;
            m_aud.Play();
        }

        // 오른쪽 또는 왼쪽으로 이동합니다.
        int key = 0; // key 변수를 초기화합니다.
        // 오른쪽으로 이동
        if (Input.GetKey(KeyCode.RightArrow)) // 만약 오른쪽 화살표 키가 눌렸다면 key 값을 1로 설정합니다.
        {
            key = 1;
        }
        // 왼쪽으로 이동     
        if (Input.GetKey(KeyCode.LeftArrow)) // 만약 왼쪽 화살표 키가 눌렸다면 key 값을 -1로 설정합니다.
        {
            key = -1;
        }
        // 플레이어의 이동 속도
        float fPlayerMoveSpeed = Mathf.Abs(m_rigid2D.velocity.x);

        // 이동 속도 제한
        if (fPlayerMoveSpeed < fmaxWalkSpeed)
        {
            m_rigid2D.AddForce(transform.right * key * fwalkForce);
        }

        // 이동 방향에 따라 플레이어 반전
        if (key != 0)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player"); // "Player" 태그를 가진 게임 오브젝트를 찾음
            Vector3 scale = player.transform.localScale; // 게임 오브젝트의 현재 스케일 값을 가져옴
            scale.x = key; // x 축의 스케일 값을 입력받은 key 값으로 설정하여 좌우 반전
            player.transform.localScale = scale; // 변경된 스케일 값을 게임 오브젝트의 스케일로 업데이트
        }

        // 플레이어 속도에 맞춰 애니메이션 속도 조절
        if (m_rigid2D.velocity.y == 0) // 만약 플레이어의 수직 속도가 0인 경우 (지면에 닿아있는 경우),
        {
            m_animator.speed = fPlayerMoveSpeed / 2.0f; // 애니메이션 속도를 플레이어의 수평 속도의 절반으로 설정합니다.
        }
        else // 그렇지 않은 경우 (공중에 있는 경우)
        {
            m_animator.speed = 1.0f; // 애니메이션 속도를 1.0으로 설정합니다.
        }

        // 플레이어가 화면 밖으로 나갔다면 처음부터
        if (transform.position.y < -10)
        {
            // 코인 개수를 0으로 초기화하고 "GameScene"을 다시 로드합니다.
            Score.coinAmount = 0;
            SceneManager.LoadScene("GameScene");
        }
    }

    // 오브젝트와 충돌하면 호출되는 함수
    void OnTriggerEnter2D(Collider2D collider)
    {
        string tag = collider.gameObject.tag;

        switch (tag)
        {
            case "Coin":
                m_aud.clip = coin;
                break;
            case "Heal":
                m_aud.clip = heal;
                break;
            default:
                return; // 처리할 태그가 없으면 바로 종료합니다.
        }

        m_aud.Play();
    }


    // 오브젝트와 충돌하면 호출되는 함수
    void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;

        switch (tag)
        {
            case "Enemy":
                OnDamaged(collision.transform.position);
                GameObject director = GameObject.Find("GameDirector");
                GameDirector gameDirector = director.GetComponent<GameDirector>();
                gameDirector.DecreaseHp();
                m_aud.clip = hit;
                break;
            case "Flag":
                Debug.Log("다음씬");
                SceneManager.LoadScene("GameScene2");
                m_aud.clip = clear;
                break;
            case "Ufo":
                Debug.Log("게임종료");
                SceneManager.LoadScene("ClearScene");
                m_aud.clip = clear;
                break;
            default:
                return;
        }
        m_aud.Play();
    }

    //충돌시 무적시간 설정
    void OnDamaged(Vector2 targetPos)
    {
        gameObject.layer = 7; // 플레이어의 레이어가 7번 레이어로 변경
        m_spriteRenderer.color = new Color(1, 1, 1, 0.4f); //무적시간일 때 플레이어가 투명하게 색 변경
        //충돌시 튕겨나가게 설정
        int direction = transform.position.x - targetPos.x > 0 ? 1 : -1; // 오른쪽으로 맞으면 오른쪽으로 튕겨나가고 왼쪽으로 맞으면 왼쪽으로 튕겨나가게 방향 설정
        m_rigid2D.AddForce(new Vector2(direction, 1) * 4, ForceMode2D.Impulse); // 플레이어를 튕겨나가는 힘 가하기
        m_animator.SetTrigger("doDamaged"); // doDamaged 트리거를 설정하여 충돌 애니메이션을 재생합니다.
        Invoke("OffDamaged", 2f); // 일정 시간 후 피격 상태 해제
    }
    //무적시간 해제
    void OffDamaged()
    {
        gameObject.layer = 6; // 플레이어의 레이어가 6번 레이어로 변경
        m_spriteRenderer.color = new Color(1, 1, 1, 1); // 플레이어 색상 복구
    }
}