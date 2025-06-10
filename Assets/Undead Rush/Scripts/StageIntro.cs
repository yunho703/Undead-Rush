using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class StageIntro : MonoBehaviour
{
    public Text countdownText;
    public GameObject countdownPanel;

    public void StartCountdown(int characterId)
    {
        
        countdownPanel.SetActive(true);
        StartCoroutine(CountdownRoutine(characterId));
    }

    IEnumerator CountdownRoutine(int characterId)
    {
        

        string[] countdowns = { "3", "2", "1", "START!" };


        foreach (string count in countdowns)
        {
            

            countdownText.text = count;

            switch (count)
            {
                case "3":
                    countdownText.color = new Color32(255, 200, 200, 0); // 연분홍
                    break;
                case "2":
                    countdownText.color = new Color32(255, 240, 180, 0); // 연노랑
                    break;
                case "1":
                    countdownText.color = new Color32(200, 200, 255, 0); // 연보라
                    break;
                case "START!":
                    countdownText.color = new Color32(100, 255, 100, 0); // 연초록
                    break;
            }
            countdownText.transform.localScale = Vector3.zero;

            countdownText.DOFade(1f, 0.3f).SetUpdate(true);
            countdownText.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack).SetUpdate(true);

            yield return new WaitForSecondsRealtime(1f);
        }

        countdownPanel.SetActive(false);
        



        GameManager.instance.GameStart(characterId); //  여기서 플레이어 등장
        GameManager.instance.Resume(); // 이제 게임 시작!
    }
}