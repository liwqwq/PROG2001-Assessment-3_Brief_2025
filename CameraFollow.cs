using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("����Ŀ��")]
    public Transform target;  // ��������ˣ�����������岿�֣�

    [Header("���ƫ��")]
    public Vector3 positionOffset = new Vector3(0, 3f, -5f); // �������ڻ����˵�λ��ƫ�ƣ�Ĭ�ϵ����˳��ӽǣ�

    void LateUpdate()
    {
        if (target == null) return;

        // 1. ����Ŀ��λ�ã���������ת��
        transform.position = target.position + positionOffset;

        // 2. �̶��ӽǣ����������ת��
        transform.rotation = Quaternion.Euler(0f, 270f, 0f); // �̶��Ƕȣ�����30�ȸ��ӽǣ�
    }
}
