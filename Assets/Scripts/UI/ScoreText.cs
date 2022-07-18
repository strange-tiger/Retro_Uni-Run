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

    // 점수를 증가시키는 메서드
    public void UpdateText(int score) => scoreText.text = $"SCORE: {score}";
}