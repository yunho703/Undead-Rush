using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSceneSetup : MonoBehaviour
{
    public GameObject gameStartButton;

    void Start()
    {
        if (gameStartButton != null && !gameStartButton.activeSelf)
        {
            gameStartButton.SetActive(true);
        }

        Time.timeScale = 1f; // ������ ���� �ִ� ��츦 ����Ͽ� �ð� �������� �ʱ�ȭ
    }
}