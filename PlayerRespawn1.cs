using UnityEngine;
using System.Collections; // 添加这行解决错误

[RequireComponent(typeof(Rigidbody))]
public class PlayerRespawn1 : MonoBehaviour 
{
    [Header("基础设置")]
    public Vector3 spawnOffset = new Vector3(0, 1f, 0);
    public float invincibleTime = 2f;

    [Header("特效")]
    public ParticleSystem respawnEffect;
    public AudioClip respawnSound;

    private Vector3 _currentSpawn;
    private Rigidbody _rb;
    private bool _isInvincible;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        SetNewSpawn(transform.position);
    }

    public void SetNewSpawn(Vector3 newPosition)
    {
        _currentSpawn = newPosition + spawnOffset;
    }

    public void ExecuteRespawn()
    {
        if (_isInvincible) return;

        if (respawnEffect != null)
            Instantiate(respawnEffect, transform.position, Quaternion.identity);

        if (respawnSound != null)
            AudioSource.PlayClipAtPoint(respawnSound, transform.position);

        transform.position = _currentSpawn;

        if (_rb != null)
        {
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
        }

        StartCoroutine(InvincibleRoutine()); // 这里需要IEnumerator
    }

    // 协程方法必须返回IEnumerator
    IEnumerator InvincibleRoutine()
    {
        _isInvincible = true;
        yield return new WaitForSeconds(invincibleTime);
        _isInvincible = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(_currentSpawn, Vector3.one);
    }
}