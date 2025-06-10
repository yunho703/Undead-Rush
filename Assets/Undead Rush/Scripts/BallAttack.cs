using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAttack : MonoBehaviour
{

    public AudioClip hitSound;               // Ÿ�� ����
    public float shortDuration = 0.4f;       // ���� ��� �ð�
    public float hitCooldown = 1.0f;         // ���� �ߺ� ���� �ð�

    private AudioSource audioSource;
    private float lastHitTime = -999f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (Time.time - lastHitTime > hitCooldown)
            {
                Debug.Log("HIT SOUND");
                StartCoroutine(PlayShortSound());
                lastHitTime = Time.time;
            }
        }
    }

    IEnumerator PlayShortSound()
    {
        audioSource.clip = hitSound;
        audioSource.Play();
        yield return new WaitForSeconds(shortDuration);
        audioSource.Stop(); // 0.1�� ��� �� ���� ����
    }
}