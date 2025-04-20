using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RobotController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;          // �ƶ��ٶ�
    public float jumpForce = 7f;          // ��Ծ����
    public float rotationSpeed = 10f;     // ת���ٶȣ�Խ��ת��Խ�죩
    public float groundCheckDistance = 0.1f; // ���������
    public LayerMask groundLayer;         // ����㼶

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // ��ֹ������ת���µ���
    }

    void Update()
    {
        // ����Ƿ��ڵ���
        isGrounded = Physics.Raycast(
            transform.position,
            Vector3.down,
            groundCheckDistance,
            groundLayer
        );

        // ��Ծ
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        // ��ȡ����
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // �����ƶ����򣨻�����������ϵ��
        Vector3 movement = new Vector3(horizontal, 0f, vertical).normalized;
        Vector3 moveVelocity = movement * moveSpeed;

        // ��������룬����ת��ɫ�����ƶ�����
        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.fixedDeltaTime
            );
        }

        // Ӧ���ƶ��ٶȣ�����Y���ٶ���֧����Ծ/������
        rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);
    }

    // ���ӻ��������ߣ������ã�
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
    }
}
