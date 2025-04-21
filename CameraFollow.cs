using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("跟随设置")]
    public Transform target;
    public Vector3 positionOffset = new Vector3(0, 3f, -5f);
    public float rotationY = 270f; // 固定Y轴旋转
    public float smoothSpeed = 5f; // 新增平滑过渡

    [Header("调试")]
    public bool showDebugRay = true;

    void LateUpdate()
    {
        if (target == null) return;

        // 计算目标位置和旋转
        Vector3 desiredPosition = target.position + positionOffset;
        Quaternion desiredRotation = Quaternion.Euler(0, rotationY, 0);

        // 平滑过渡
        transform.position = Vector3.Lerp(
            transform.position, 
            desiredPosition, 
            smoothSpeed * Time.deltaTime
        );
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            desiredRotation,
            smoothSpeed * Time.deltaTime
        );

        // 调试可视化
        if (showDebugRay)
        {
            Debug.DrawRay(transform.position, transform.forward * 5f, Color.blue);
            Debug.DrawLine(transform.position, target.position, Color.red);
        }
    }

    // 外部重置方法
    public void ResetToDefault()
    {
        if (target != null)
        {
            transform.position = target.position + positionOffset;
            transform.rotation = Quaternion.Euler(0, rotationY, 0);
        }
    }
}
