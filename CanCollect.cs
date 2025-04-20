using UnityEngine;

public class CanCollect : MonoBehaviour
{
    public AudioClip collectSound;  // ����÷���Ч�ļ����磺coin.wav��
    public int scoreToAdd = 100;    // ���ε÷�ֵ

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // ȷ�������"Player"��ǩ
        {
            // 1. ������Ч
            AudioSource.PlayClipAtPoint(collectSound, transform.position);

            // 2. ���µ÷�
            ScoreManager.Instance.AddScore(scoreToAdd);

            // 3. ���ٹ���
            Destroy(gameObject);
        }
    }
}
