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

        Time.timeScale = 1f; // 게임이 멈춰 있는 경우를 대비하여 시간 스케일을 초기화
    }
}