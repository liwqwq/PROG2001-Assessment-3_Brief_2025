using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("跟随目标")]
    public Transform target;  // 拖入机器人（或机器人身体部分）

    [Header("相机偏移")]
    public Vector3 positionOffset = new Vector3(0, 3f, -5f); // 相机相对于机器人的位置偏移（默认第三人称视角）

    void LateUpdate()
    {
        if (target == null) return;

        // 1. 跟随目标位置（不跟随旋转）
        transform.position = target.position + positionOffset;

        // 2. 固定视角（不随鼠标旋转）
        transform.rotation = Quaternion.Euler(0f, 270f, 0f); // 固定角度（例如30度俯视角）
    }
}
