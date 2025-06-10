using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeaponSound : MonoBehaviour
{
    public AudioClip shootSound;           // Inspector에 넣는 사운드
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Fire()
    {
        // 원거리 발사 로직 (총알 생성 등)

        // 사운드 재생
        audioSource.PlayOneShot(shootSound);
    }
}