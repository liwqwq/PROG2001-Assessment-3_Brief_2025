using UnityEngine;

public class CameraSwitchOnCollision : MonoBehaviour
{
    public Camera firstCamera; // 第一个摄像头
    public Camera secondCamera; // 第二个摄像头
    public string targetTag = "Player"; // 目标标签（可根据需要设置，默认为 "Player"）

    void Start()
    {
        // 初始化时禁用第二个摄像头，启用第一个摄像头
        if (firstCamera != null)
            firstCamera.gameObject.SetActive(true);

        if (secondCamera != null)
            secondCamera.gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        // 当碰撞发生时，如果碰撞对象是目标物体（如Player），切换摄像头
        if (collision.gameObject.CompareTag(targetTag))
        {
            SwitchCameras();
        }
    }

    void SwitchCameras()
    {
        // 切换摄像头的激活状态
        if (firstCamera != null && secondCamera != null)
        {
            bool isFirstActive = firstCamera.gameObject.activeSelf;
            firstCamera.gameObject.SetActive(!isFirstActive);
            secondCamera.gameObject.SetActive(isFirstActive);
        }
    }
}
