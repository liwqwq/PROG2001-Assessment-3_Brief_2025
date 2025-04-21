using UnityEngine;

public class DeadlyObstacle : MonoBehaviour
{
    [Header("重生点设置")]
    public bool useCustomRespawn = false;
    public Transform customRespawnPoint; // 可指定特定重生点

    [Header("碰撞设置")]
    public bool isTrigger = true;
    public string playerTag = "Player";

    void OnTriggerEnter(Collider other)
    {
        if (!isTrigger) return;
        HandleCollision(other.gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isTrigger) return;
        HandleCollision(collision.gameObject);
    }

    void HandleCollision(GameObject obj)
    {
        if (obj.CompareTag(playerTag))
        {
            var player = obj.GetComponent<PlayerRespawn1>();
            if (player != null)
            {
                // 使用自定义重生点或保持玩家当前重生点
                if (useCustomRespawn && customRespawnPoint != null)
                    player.SetNewSpawn(customRespawnPoint.position);
                
                player.ExecuteRespawn();
            }
        }
    }

    // 可视化自定义重生点
    void OnDrawGizmos()
    {
        if (useCustomRespawn && customRespawnPoint != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, customRespawnPoint.position);
            Gizmos.DrawSphere(customRespawnPoint.position, 0.3f);
        }
    }
}