using UnityEngine;

// 왼쪽 끝으로 이동한 배경을 오른쪽 끝으로 재배치하는 스크립트
public class BackgroundLoop : MonoBehaviour {
    private float width; // 배경의 가로 길이
    private Vector2 _resetPosition;

    private void Awake() {
        // 가로 길이를 측정하는 처리
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        width = boxCollider.size.x;

        _resetPosition = new Vector2(width, 0f);
    }

    private void Update() {
        // 현재 위치가 원점에서 왼쪽으로 width 이상 이동했을때 위치를 리셋
        if (transform.position.x <= -width)
        {
            transform.position = _resetPosition;
        }
    }
}