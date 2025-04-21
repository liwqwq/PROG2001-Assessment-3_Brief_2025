using UnityEngine;
using UnityEngine.UI;  // 引入UI命名空间

public class TrophySystem : MonoBehaviour
{
    public int playerScore;  // 玩家分数
    public Image trophyImage; // 用于显示奖杯的UI Image
    public Text scoreText;  // 显示分数的Text组件
    public GameObject trophyPanel;  // 显示奖杯的面板

    // 三个不同的奖杯图标
    public Sprite goldTrophy;
    public Sprite silverTrophy;
    public Sprite bronzeTrophy;

    // 关闭按钮的功能
    public Button closeButton;

    void Start()
    {
        // 关闭按钮的点击事件
        closeButton.onClick.AddListener(CloseTrophyPanel);

        // 根据分数颁发奖杯并显示
        AwardTrophy(playerScore);
    }

    void AwardTrophy(int score)
    {
        // 判断分数并设置奖杯图标
        if (score >= 90)
        {
            trophyImage.sprite = goldTrophy;
        }
        else if (score >= 80)
        {
            trophyImage.sprite = silverTrophy;
        }
        else if (score >= 70)
        {
            trophyImage.sprite = bronzeTrophy;
        }
        else
        {
            trophyImage.sprite = null; // 没有奖杯
        }

        // 显示分数
        scoreText.text = "你的分数是：" + score.ToString();

        // 激活奖杯面板
        trophyPanel.SetActive(true);
    }

    void CloseTrophyPanel()
    {
        // 关闭奖杯面板
        trophyPanel.SetActive(false);
    }
}
