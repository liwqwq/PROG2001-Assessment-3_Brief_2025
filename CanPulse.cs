using UnityEngine;

public class CanPulse : MonoBehaviour
{
    [Header("闪烁参数")]
    public float pulseSpeed = 1.5f;           // 闪烁速度
    public Color brightColor = Color.white;   // 亮色（如浅黄）
    public Color darkColor = Color.gray;      // 暗色（如深灰）
    [Range(0.1f, 2f)] public float contrast = 0.8f; // 对比度

    private Material instanceMat;
    private Color originalColor;

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        instanceMat = renderer.material;      // 创建独立材质实例
        originalColor = instanceMat.color;
    }

    void Update()
    {
        // 计算当前颜色插值（正弦波）
        float t = Mathf.PingPong(Time.time * pulseSpeed, 1f);
        Color pulseColor = Color.Lerp(darkColor, brightColor, t * contrast);

        // 混合原始颜色与脉冲颜色
        instanceMat.color = originalColor * pulseColor;
    }

    void OnDestroy()
    {
        if (instanceMat != null)
            Destroy(instanceMat);
    }
}