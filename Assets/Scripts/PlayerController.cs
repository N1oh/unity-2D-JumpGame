using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    AudioSource m_aud; // AudioSource ������ �����մϴ�.
    Rigidbody2D m_rigid2D;  // Rigidbody2D ������ �����մϴ�.
    Animator m_animator;  //Animator ������ �����մϴ�.
    SpriteRenderer m_spriteRenderer; // SpriteRenderer ������ �����մϴ�.

    [SerializeField]
    float fjumpForce = 680.0f; // �÷��̾�� ���� ���� ��

    [SerializeField]
    float fwalkForce = 30.0f; // �÷��̾� �̵��� ���� ��

    [SerializeField]
    float fmaxWalkSpeed = 2.0f; // �̵� �ӵ��� �ִ밪

    public AudioClip jump; // ���� ����
    public AudioClip hit; // �浹 ����
    public AudioClip coin; // ���� ȹ�� ����
    public AudioClip heal; // ȸ�� ������ ȹ�� ����
    public AudioClip clear; // Ŭ���� ����

    void Start()
    {
        m_rigid2D = GetComponent<Rigidbody2D>(); // Rigidbody2D ������Ʈ ��������
        m_animator = GetComponent<Animator>(); // Animator ������Ʈ ��������
        m_spriteRenderer = GetComponent<SpriteRenderer>(); // SpriteRenderer ������Ʈ ��������
        m_aud = GetComponent<AudioSource>(); // AudioSource ������Ʈ ��������
    }

    void Update()
    {
        // ���� ������ ó���մϴ�. �÷��̾��� ���� �ӵ��� 0�� ��쿡�� ����˴ϴ�.
        if (Input.GetKeyDown(KeyCode.Space) && m_rigid2D.velocity.y == 0)
        {
            // fjumpForce ��ŭ�� ���� ���� �������� �����ݴϴ�.
            m_rigid2D.AddForce(transform.up * fjumpForce);
            // jump ����� Ŭ���� �����ϰ� ����մϴ�.
            m_aud.clip = jump;
            m_aud.Play();
        }

        // ������ �Ǵ� �������� �̵��մϴ�.
        int key = 0; // key ������ �ʱ�ȭ�մϴ�.
        // ���������� �̵�
        if (Input.GetKey(KeyCode.RightArrow)) // ���� ������ ȭ��ǥ Ű�� ���ȴٸ� key ���� 1�� �����մϴ�.
        {
            key = 1;
        }
        // �������� �̵�     
        if (Input.GetKey(KeyCode.LeftArrow)) // ���� ���� ȭ��ǥ Ű�� ���ȴٸ� key ���� -1�� �����մϴ�.
        {
            key = -1;
        }
        // �÷��̾��� �̵� �ӵ�
        float fPlayerMoveSpeed = Mathf.Abs(m_rigid2D.velocity.x);

        // �̵� �ӵ� ����
        if (fPlayerMoveSpeed < fmaxWalkSpeed)
        {
            m_rigid2D.AddForce(transform.right * key * fwalkForce);
        }

        // �̵� ���⿡ ���� �÷��̾� ����
        if (key != 0)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player"); // "Player" �±׸� ���� ���� ������Ʈ�� ã��
            Vector3 scale = player.transform.localScale; // ���� ������Ʈ�� ���� ������ ���� ������
            scale.x = key; // x ���� ������ ���� �Է¹��� key ������ �����Ͽ� �¿� ����
            player.transform.localScale = scale; // ����� ������ ���� ���� ������Ʈ�� �����Ϸ� ������Ʈ
        }

        // �÷��̾� �ӵ��� ���� �ִϸ��̼� �ӵ� ����
        if (m_rigid2D.velocity.y == 0) // ���� �÷��̾��� ���� �ӵ��� 0�� ��� (���鿡 ����ִ� ���),
        {
            m_animator.speed = fPlayerMoveSpeed / 2.0f; // �ִϸ��̼� �ӵ��� �÷��̾��� ���� �ӵ��� �������� �����մϴ�.
        }
        else // �׷��� ���� ��� (���߿� �ִ� ���)
        {
            m_animator.speed = 1.0f; // �ִϸ��̼� �ӵ��� 1.0���� �����մϴ�.
        }

        // �÷��̾ ȭ�� ������ �����ٸ� ó������
        if (transform.position.y < -10)
        {
            // ���� ������ 0���� �ʱ�ȭ�ϰ� "GameScene"�� �ٽ� �ε��մϴ�.
            Score.coinAmount = 0;
            SceneManager.LoadScene("GameScene");
        }
    }

    // ������Ʈ�� �浹�ϸ� ȣ��Ǵ� �Լ�
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
                return; // ó���� �±װ� ������ �ٷ� �����մϴ�.
        }

        m_aud.Play();
    }


    // ������Ʈ�� �浹�ϸ� ȣ��Ǵ� �Լ�
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
                Debug.Log("������");
                SceneManager.LoadScene("GameScene2");
                m_aud.clip = clear;
                break;
            case "Ufo":
                Debug.Log("��������");
                SceneManager.LoadScene("ClearScene");
                m_aud.clip = clear;
                break;
            default:
                return;
        }
        m_aud.Play();
    }

    //�浹�� �����ð� ����
    void OnDamaged(Vector2 targetPos)
    {
        gameObject.layer = 7; // �÷��̾��� ���̾ 7�� ���̾�� ����
        m_spriteRenderer.color = new Color(1, 1, 1, 0.4f); //�����ð��� �� �÷��̾ �����ϰ� �� ����
        //�浹�� ƨ�ܳ����� ����
        int direction = transform.position.x - targetPos.x > 0 ? 1 : -1; // ���������� ������ ���������� ƨ�ܳ����� �������� ������ �������� ƨ�ܳ����� ���� ����
        m_rigid2D.AddForce(new Vector2(direction, 1) * 4, ForceMode2D.Impulse); // �÷��̾ ƨ�ܳ����� �� ���ϱ�
        m_animator.SetTrigger("doDamaged"); // doDamaged Ʈ���Ÿ� �����Ͽ� �浹 �ִϸ��̼��� ����մϴ�.
        Invoke("OffDamaged", 2f); // ���� �ð� �� �ǰ� ���� ����
    }
    //�����ð� ����
    void OffDamaged()
    {
        gameObject.layer = 6; // �÷��̾��� ���̾ 6�� ���̾�� ����
        m_spriteRenderer.color = new Color(1, 1, 1, 1); // �÷��̾� ���� ����
    }
}