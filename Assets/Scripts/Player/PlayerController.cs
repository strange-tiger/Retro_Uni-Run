using UnityEngine;

// PlayerController는 플레이어 캐릭터로서 Player 게임 오브젝트를 제어한다.
public class PlayerController : MonoBehaviour
{
    public AudioClip deathClip; // 사망시 재생할 오디오 클립
    public float jumpForce = 700f; // 점프 힘
    public int MaxJumpCount = 2;

    private int jumpCount = 0; // 누적 점프 횟수
    private bool isOnGround = false; // 바닥에 닿았는지 나타냄
    private bool isDead = false; // 사망 상태

    private Rigidbody2D playerRigidbody; // 사용할 리지드바디 컴포넌트
    private Animator animator; // 사용할 애니메이터 컴포넌트
    private AudioSource playerAudio; // 사용할 오디오 소스 컴포넌트
    private Vector2 _zero;

    // C#에서 상수 만드는 방법
    // 1. cosnt : 컴파일에 평가
    // 2. readonly : 런타임에 평가
    // 둘의 차이 : 평가 시점
    private static class AnimationID
    {
        public static readonly int IS_ON_GROUND = Animator.StringToHash("IsOnGround");
        // string을 참조하지 않기 위해 해시 값으로 변환 // 최적화, 리팩토링에 해당하는 부분
        public static readonly int DIE = Animator.StringToHash("Die");
        // string을 참조하지 않기 위해 해시 값으로 변환 // 최적화, 리팩토링에 해당하는 부분
    }

    private static readonly float MIN_NORMAL_Y = Mathf.Sin(45f * Mathf.Deg2Rad);
    
    private void Awake()
    {
        // 초기화
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        _zero = Vector2.zero;
    }

    private void Update()
    {
        if (isDead)
        {
            return;
        }

        // 사용자 입력을 감지하고 점프하는 처리
        if (Input.GetMouseButtonDown(0))
        {
            // 최대 점프에 도달했으면 아무것도 안 함.
            if (jumpCount >= MaxJumpCount)
            {
                return;
            }

            ++jumpCount;
            playerRigidbody.velocity = _zero;
            playerRigidbody.AddForce(new Vector2(0f, jumpForce));
            playerAudio.Play();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (playerRigidbody.velocity.y > 0)
            {
                playerRigidbody.velocity *= 0.5f;
            }
        }

        animator.SetBool(AnimationID.IS_ON_GROUND, isOnGround); // string을 참조하지 않고 해시 키를 적용, 더욱 빠른 할당 // 최적화, 리팩토링에 해당하는 부분
    }

    private void Die()
    {
        // 사망 처리
        // 1. isDead = true
        isDead = true;

        // 2. 애니메이션 업데이트
        animator.SetTrigger(AnimationID.DIE);

        // 3. 플레이어 캐릭터 멈추기
        playerRigidbody.velocity = _zero;

        // 4. 죽을 때 소리 재생
        playerAudio.PlayOneShot(deathClip);

        GameManager.Instance.OnPlayerDead();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(isDead)
        {
            return;
        }

        // 트리거 콜라이더를 가진 장애물과의 충돌을 감지
        if (other.tag == "Dead")
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 바닥에 닿았음을 감지하는 처리
        ContactPoint2D point = collision.GetContact(0);
        if (point.normal.y >= MIN_NORMAL_Y)
        {
            isOnGround = true;
            jumpCount = 0;

            // GameManager.Instance.AddScore();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 바닥에서 벗어났음을 감지하는 처리
        isOnGround = false;
    }
}