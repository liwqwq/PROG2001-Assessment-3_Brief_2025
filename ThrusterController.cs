using UnityEngine;

public class ThrusterController : MonoBehaviour
{
    [Header("Particle Settings")]
    public ParticleSystem thrusterParticles;  // 拖入粒子系统
    public float minEmissionRate = 10f;       // 最小粒子发射率
    public float maxEmissionRate = 50f;       // 最大粒子发射率
    public float maxSpeed = 10f;              // 最大移动速度阈值

    [Header("Audio Settings")]
    public AudioClip thrusterSound;           // 拖入喷射音效
    [Range(0, 1)]
    public float volume = 0.5f;               // 音效音量

    private Rigidbody rb;
    private ParticleSystem.EmissionModule emissionModule;
    private AudioSource audioSource;

    void Start()
    {
        // 初始化组件
        rb = GetComponent<Rigidbody>();

        // 粒子系统设置
        if (thrusterParticles != null)
        {
            emissionModule = thrusterParticles.emission;
            emissionModule.rateOverTime = 0;  // 初始关闭
        }
        else
        {
            Debug.LogError("未分配粒子系统！");
        }

        // 音效设置
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = thrusterSound;
        audioSource.loop = true;              // 循环播放
        audioSource.volume = volume;
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (thrusterParticles == null || audioSource == null) return;

        // 获取当前速度
        float speed = rb.velocity.magnitude;
        bool isMoving = speed > 0.1f;

        // 控制粒子喷射
        if (isMoving)
        {
            // 动态调整粒子发射率
            float emissionRate = Mathf.Lerp(minEmissionRate, maxEmissionRate, speed / maxSpeed);
            emissionModule.rateOverTime = emissionRate;

            // 确保粒子在播放状态
            if (!thrusterParticles.isPlaying)
                thrusterParticles.Play();
        }
        else
        {
            // 停止粒子
            emissionModule.rateOverTime = 0;
            if (thrusterParticles.isPlaying)
                thrusterParticles.Stop();
        }

        // 控制音效
        if (isMoving)
        {
            if (!audioSource.isPlaying)
                audioSource.Play();  // 移动时播放音效
        }
        else
        {
            if (audioSource.isPlaying)
                audioSource.Stop();  // 停止时关闭音效
        }
    }

    // 调试用：显示当前速度
    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 200, 20), $"速度: {rb.velocity.magnitude:F2}");
    }
}