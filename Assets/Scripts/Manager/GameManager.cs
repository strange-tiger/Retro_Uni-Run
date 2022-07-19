using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// 게임 오버 상태를 표현하고, 게임 점수와 UI를 관리하는 게임 매니저
// 씬에는 단 하나의 게임 매니저만 존재할 수 있다.
public class GameManager : SingletonBehaviour<GameManager> 
{
    public int ScoreIncreaseAmount = 1;
    // public GameObject gameoverUI; // 게임 오버시 활성화 할 UI 게임 오브젝트

    public UnityEvent OnGameOver = new UnityEvent();                // 옵저버 패턴 인스턴스 Subject

    public UnityEvent<int> OnScoreChanged = new UnityEvent<int>();  // 옵저버 패턴 인스턴스 Subject
    // public event UnityAction<int> OnScoreChanged2;
    public int CurrentScore                                         // 옵저버 패턴 인스턴스 Observer
    {
        get
        {
            return _currentScore;
        }
        set
        {
            _currentScore = value;
            OnScoreChanged.Invoke(_currentScore); // 갱신 통지
        }
    }

    
    private int _currentScore = 0;
    public bool _isGameover = false; // 게임 오버 상태

    void Update() 
    {
        // 게임 오버 상태에서 게임을 재시작할 수 있게 하는 처리
        if (!_isGameover)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            reset();
            SceneManager.LoadScene(0); // MainScene
        }
    }

    public void AddScore()
    {
        CurrentScore += ScoreIncreaseAmount;
    }

    // 플레이어 캐릭터가 사망시 게임 오버를 실행하는 메서드
    public void OnPlayerDead() 
    {
        _isGameover = true;
        OnGameOver.Invoke();
        // gameoverUI.SetActive(true);
    }

    private void reset()
    {
        _currentScore = 0;
        _isGameover = false;
    }
}