using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RobotController : MonoBehaviour
{
    [Header("控制状态")]
    public bool isControllable = false;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;          // 移动速度
    public float jumpForce = 7f;          // 跳跃力
    public float rotationSpeed = 120f;    // 旋转速度
    public float groundCheckDistance = 0.1f; // 地面检测距离
    public LayerMask groundLayer;         // 地面层级

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // 防止物理旋转导致的倾斜
    }

    void Update()
    {
        if (!isControllable) return;

        // 检测是否在地面
        isGrounded = Physics.Raycast(
            transform.position,
            Vector3.down,
            groundCheckDistance,
            groundLayer
        );

        // 跳跃
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        if (!isControllable) return;

        // 获取输入
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // 计算移动方向（基于世界坐标系）
        Vector3 movement = new Vector3(horizontal, 0f, vertical).normalized;
        Vector3 moveVelocity = movement * moveSpeed;

        // 如果有移动输入，则旋转角色朝向移动方向
        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.fixedDeltaTime
            );
        }

        // 应用移动速度，保留Y轴速度以支持跳跃/重力
        rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);
    }

    // 绘制地面检测线（调试用）
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
    }

    public void EnableControl()
    {
        isControllable = true;
        Debug.Log($"{name} 控制已启用");
    }

    public void DisableControl()
    {
        isControllable = false;
        Debug.Log($"{name} 控制已禁用");
    }
}
