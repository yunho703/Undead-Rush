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
                    countdownText.color = new Color32(255, 200, 200, 0); // ����ȫ
                    break;
                case "2":
                    countdownText.color = new Color32(255, 240, 180, 0); // �����
                    break;
                case "1":
                    countdownText.color = new Color32(200, 200, 255, 0); // ������
                    break;
                case "START!":
                    countdownText.color = new Color32(100, 255, 100, 0); // ���ʷ�
                    break;
            }
            countdownText.transform.localScale = Vector3.zero;

            countdownText.DOFade(1f, 0.3f).SetUpdate(true);
            countdownText.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack).SetUpdate(true);

            yield return new WaitForSecondsRealtime(1f);
        }

        countdownPanel.SetActive(false);
        



        GameManager.instance.GameStart(characterId); //  ���⼭ �÷��̾� ����
        GameManager.instance.Resume(); // ���� ���� ����!
    }
}