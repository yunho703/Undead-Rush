using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CharacterGroupEffect : MonoBehaviour
{
    public GameObject[] characterCards;
    public GameObject characterSelectionUI;
    public GameObject countdownUI;
    public StageIntro stageIntro;

    private int selectedIndex = -1;

    public void OnSelectCharacter(int index)
    {
        if (selectedIndex == index)
            return;

        selectedIndex = index;

        for (int i = 0; i < characterCards.Length; i++)
        {
            Transform card = characterCards[i].transform;
            CanvasGroup cg = characterCards[i].GetComponent<CanvasGroup>();

            if (i == selectedIndex)
            {
                card.localScale = Vector3.one * 1.2f;
                cg.alpha = 1f;
            }
            else
            {
                card.localScale = Vector3.one * 0.8f;
                cg.alpha = 0.3f;
            }
        }

        //  게임 정지하고
        GameManager.instance.Stop();

        //  UI 전환
        characterSelectionUI.SetActive(false);
        countdownUI.SetActive(true);

        //  카운트다운 시작
        stageIntro.StartCountdown(index); // 캐릭터 ID 넘김
    }
}