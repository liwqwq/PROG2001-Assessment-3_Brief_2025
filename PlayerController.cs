using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [Header("控制状态")]
    public bool isControllable = false;

    [Header("Movement Settings")]
    [Tooltip("移动速度")] 
    public float moveSpeed = 5f;
    [Tooltip("转向速度（度/秒）")] 
    public float rotateSpeed = 120f; // 调整为120以匹配原值
    [Tooltip("跳跃力度")] 
    public float jumpForce = 5f;
    [Tooltip("地面检测距离")] 
    public float groundCheckDistance = 0.2f;
    [Tooltip("移动动画切换阈值")] 
    public float movementThreshold = 0.1f;

    // 组件引用
    private Rigidbody rb;
    private Animator animator;
    
    // 输入缓存
    private float verticalInput;
    private float horizontalInput;
    
    // 状态标记
    private bool isGrounded;
    private bool isJumping;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        
        // 物理设置
        rb.freezeRotation = true; // 防止物理旋转导致摔倒
    }

    void Update()
    {
        if (!isControllable) return;

        // 1. 获取输入
        GetPlayerInput();
        
        // 2. 地面检测
        CheckGroundStatus();
        
        // 3. 动画控制
        ControlAnimations();
        
        // 4. 跳跃检测
        HandleJump();
    }

    void FixedUpdate()
    {
        if (!isControllable) return;

        // 物理相关的移动和旋转
        HandleMovement();
        HandleRotation();
    }

    #region 核心方法
    private void GetPlayerInput()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void CheckGroundStatus()
    {
        isGrounded = Physics.Raycast(
            transform.position, 
            Vector3.down, 
            groundCheckDistance
        );
        
        // 落地时重置跳跃状态
        if(isGrounded && isJumping)
        {
            isJumping = false;
            animator.SetBool("IsJumping", false);
        }
    }

    private void ControlAnimations()
    {
        // 移动动画（考虑输入阈值）
        bool isMoving = Mathf.Abs(verticalInput) > movementThreshold;
        animator.SetBool("IsRunning", isMoving);
    }

    private void HandleJump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded && !isJumping)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger("Jump");
            animator.SetBool("IsJumping", true);
            isJumping = true;
        }
    }

    private void HandleMovement()
    {
        // 只在前向输入超过阈值时移动
        if(Mathf.Abs(verticalInput) > movementThreshold)
        {
            Vector3 moveDirection = transform.forward * verticalInput;
            rb.MovePosition(
                rb.position + 
                moveDirection * moveSpeed * Time.fixedDeltaTime
            );
        }
    }

    private void HandleRotation()
    {
        // 只在水平输入超过阈值时旋转
        if(Mathf.Abs(horizontalInput) > movementThreshold)
        {
            float rotationAmount = horizontalInput * rotateSpeed * Time.fixedDeltaTime;
            Quaternion turnRotation = Quaternion.Euler(0f, rotationAmount, 0f);
            rb.MoveRotation(rb.rotation * turnRotation);
        }
    }
    #endregion

    // 新增控制方法
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

    void OnDrawGizmosSelected()
    {
        // 可视化地面检测线（仅在选中时显示）
        Gizmos.color = Color.green;
        Gizmos.DrawLine(
            transform.position, 
            transform.position + Vector3.down * groundCheckDistance
        );
    }
}