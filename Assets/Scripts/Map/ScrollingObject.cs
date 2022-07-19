using UnityEngine;

// 게임 오브젝트를 계속 왼쪽으로 움직이는 스크립트
public class ScrollingObject : MonoBehaviour 
{
    public float speed = 10f; // 이동 속도

    private void Update() 
    {
        // 게임 오브젝트를 왼쪽으로 일정 속도로 평행 이동하는 처리
        if(_isScrolling)
            transform.Translate(-1 * speed * Time.deltaTime, 0f, 0f);
    }

    private bool _isScrolling = true;

    public void StopScrolling()
    {
        _isScrolling = false;
    }

    void OnEnable()
    {
        GameManager.Instance.OnGameOver.AddListener(StopScrolling);
    }

    void OnDisable()
    {
        GameManager.Instance.OnGameOver.RemoveListener(StopScrolling);
    }
}