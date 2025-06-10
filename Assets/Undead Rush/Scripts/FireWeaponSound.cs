using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeaponSound : MonoBehaviour
{
    public AudioClip shootSound;           // Inspector�� �ִ� ����
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Fire()
    {
        // ���Ÿ� �߻� ���� (�Ѿ� ���� ��)

        // ���� ���
        audioSource.PlayOneShot(shootSound);
    }
}