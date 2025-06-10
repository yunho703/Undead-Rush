using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAttack : MonoBehaviour
{

    public AudioClip hitSound;               // 타격 사운드
    public float shortDuration = 0.4f;       // 사운드 재생 시간
    public float hitCooldown = 1.0f;         // 사운드 중복 방지 시간

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
        audioSource.Stop(); // 0.1초 재생 후 강제 정지
    }
}