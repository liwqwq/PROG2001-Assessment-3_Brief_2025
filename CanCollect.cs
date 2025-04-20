using UnityEngine;

public class CanCollect : MonoBehaviour
{
    public AudioClip collectSound;  // 拖入得分音效文件（如：coin.wav）
    public int scoreToAdd = 100;    // 单次得分值

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 确保玩家有"Player"标签
        {
            // 1. 播放音效
            AudioSource.PlayClipAtPoint(collectSound, transform.position);

            // 2. 更新得分
            ScoreManager.Instance.AddScore(scoreToAdd);

            // 3. 销毁罐子
            Destroy(gameObject);
        }
    }
}
