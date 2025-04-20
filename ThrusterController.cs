using UnityEngine;

public class ThrusterController : MonoBehaviour
{
    [Header("Particle Settings")]
    public ParticleSystem thrusterParticles;  // ��������ϵͳ
    public float minEmissionRate = 10f;       // ��С���ӷ�����
    public float maxEmissionRate = 50f;       // ������ӷ�����
    public float maxSpeed = 10f;              // ����ƶ��ٶ���ֵ

    [Header("Audio Settings")]
    public AudioClip thrusterSound;           // ����������Ч
    [Range(0, 1)]
    public float volume = 0.5f;               // ��Ч����

    private Rigidbody rb;
    private ParticleSystem.EmissionModule emissionModule;
    private AudioSource audioSource;

    void Start()
    {
        // ��ʼ�����
        rb = GetComponent<Rigidbody>();

        // ����ϵͳ����
        if (thrusterParticles != null)
        {
            emissionModule = thrusterParticles.emission;
            emissionModule.rateOverTime = 0;  // ��ʼ�ر�
        }
        else
        {
            Debug.LogError("δ��������ϵͳ��");
        }

        // ��Ч����
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = thrusterSound;
        audioSource.loop = true;              // ѭ������
        audioSource.volume = volume;
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (thrusterParticles == null || audioSource == null) return;

        // ��ȡ��ǰ�ٶ�
        float speed = rb.velocity.magnitude;
        bool isMoving = speed > 0.1f;

        // ������������
        if (isMoving)
        {
            // ��̬�������ӷ�����
            float emissionRate = Mathf.Lerp(minEmissionRate, maxEmissionRate, speed / maxSpeed);
            emissionModule.rateOverTime = emissionRate;

            // ȷ�������ڲ���״̬
            if (!thrusterParticles.isPlaying)
                thrusterParticles.Play();
        }
        else
        {
            // ֹͣ����
            emissionModule.rateOverTime = 0;
            if (thrusterParticles.isPlaying)
                thrusterParticles.Stop();
        }

        // ������Ч
        if (isMoving)
        {
            if (!audioSource.isPlaying)
                audioSource.Play();  // �ƶ�ʱ������Ч
        }
        else
        {
            if (audioSource.isPlaying)
                audioSource.Stop();  // ֹͣʱ�ر���Ч
        }
    }

    // �����ã���ʾ��ǰ�ٶ�
    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 200, 20), $"�ٶ�: {rb.velocity.magnitude:F2}");
    }
}