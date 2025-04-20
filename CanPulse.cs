using UnityEngine;

public class CanPulse : MonoBehaviour
{
    [Header("��˸����")]
    public float pulseSpeed = 1.5f;           // ��˸�ٶ�
    public Color brightColor = Color.white;   // ��ɫ����ǳ�ƣ�
    public Color darkColor = Color.gray;      // ��ɫ������ң�
    [Range(0.1f, 2f)] public float contrast = 0.8f; // �Աȶ�

    private Material instanceMat;
    private Color originalColor;

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        instanceMat = renderer.material;      // ������������ʵ��
        originalColor = instanceMat.color;
    }

    void Update()
    {
        // ���㵱ǰ��ɫ��ֵ�����Ҳ���
        float t = Mathf.PingPong(Time.time * pulseSpeed, 1f);
        Color pulseColor = Color.Lerp(darkColor, brightColor, t * contrast);

        // ���ԭʼ��ɫ��������ɫ
        instanceMat.color = originalColor * pulseColor;
    }

    void OnDestroy()
    {
        if (instanceMat != null)
            Destroy(instanceMat);
    }
}