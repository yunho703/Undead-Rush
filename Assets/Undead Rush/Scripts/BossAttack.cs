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
        Vector3 spawnPos = transform.position + dir * 1f; // 보스 앞으로 1칸

        GameObject bullet = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);

        // Rigidbody2D에 속도 주기
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = dir * bulletSpeed;
        }
        else
        {
            Debug.LogWarning("BossBullet에 Rigidbody2D가 없습니다!");
        }

        Debug.Log("보스가 발사했습니다");
    }
}
