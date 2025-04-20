using TMPro;
using UnityEngine;
using UnityEngine.UI; // ���UI�����ռ�

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
            ForceUpdateScoreText(); // ��ʼ��ʱǿ��ˢ��
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
        // ����1��ֱ�Ӹ����ı�
        scoreText.text = "SCORE: " + currentScore;

        // ����2�����������ؽ�
        scoreText.SetAllDirty();

        // ����3��ǿ�Ʋ���ˢ��
        LayoutRebuilder.ForceRebuildLayoutImmediate(
            scoreText.rectTransform
        );

        Debug.Log($"ǿ��ˢ��UI: {scoreText.text}");
    }
}
