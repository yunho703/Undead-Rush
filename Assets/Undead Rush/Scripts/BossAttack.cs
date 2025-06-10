using System.Collections;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float fireInterval = 3f;
    public float bulletSpeed = 8f;

    private Transform player;

    void Start()
    {
        player = GameManager.instance.player.transform;
        StartCoroutine(FireRoutine());
    }

    IEnumerator FireRoutine()
    {
        while (GameManager.instance.isLive)
        {
            yield return new WaitForSeconds(fireInterval);
            Fire();
        }
    }

    void Fire()
    {
        if (!player) return;

        Vector3 dir = (player.position - transform.position).normalized;
        Vector3 spawnPos = transform.position + dir * 1f; // ���� ������ 1ĭ

        GameObject bullet = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);

        // Rigidbody2D�� �ӵ� �ֱ�
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = dir * bulletSpeed;
        }
        else
        {
            Debug.LogWarning("BossBullet�� Rigidbody2D�� �����ϴ�!");
        }

        Debug.Log("������ �߻��߽��ϴ�");
    }
}
