using TMPro;
using UnityEngine;
using UnityEngine.UI; // 添加UI命名空间

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public TMP_Text scoreText;
    private int currentScore;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            currentScore = 0;
            ForceUpdateScoreText(); // 初始化时强制刷新
        }
        else
            Destroy(gameObject);
    }

    public void AddScore(int points)
    {
        currentScore += points;
        ForceUpdateScoreText();
    }

    private void ForceUpdateScoreText()
    {
        // 方法1：直接更新文本
        scoreText.text = "SCORE: " + currentScore;

        // 方法2：触发网格重建
        scoreText.SetAllDirty();

        // 方法3：强制布局刷新
        LayoutRebuilder.ForceRebuildLayoutImmediate(
            scoreText.rectTransform
        );

        Debug.Log($"强制刷新UI: {scoreText.text}");
    }
}
