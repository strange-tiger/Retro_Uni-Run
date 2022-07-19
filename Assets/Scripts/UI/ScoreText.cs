using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    private TextMeshProUGUI scoreText;

    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        GameManager.Instance.OnScoreChanged.AddListener(UpdateText);
    }

    // 점수를 증가시키는 메서드    // 식 본문 (Expression Body)
    public void UpdateText(int score) => scoreText.text = $"SCORE: {score}";

    void OnDisable()
    {
        GameManager.Instance.OnScoreChanged.RemoveListener(UpdateText);
    }
}