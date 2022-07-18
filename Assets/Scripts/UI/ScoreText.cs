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

    // ������ ������Ű�� �޼���
    public void UpdateText(int score) => scoreText.text = $"SCORE: {score}";
}