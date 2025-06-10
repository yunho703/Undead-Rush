using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class HUD : MonoBehaviour
{
    public enum InfoType { Exp, Level, Kill, Time, Health }
    public InfoType type;

    public Text myText;
    Slider mySlider;

    // Kill용 변수
    private int currentKill = 0;
    private Tween killTween;

    private void Awake()
    {
        
        mySlider = GetComponent<Slider>();

      
    }

    void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Exp:
                float curExp = GameManager.instance.exp;
                float maxExp = GameManager.instance.nextExp[Mathf.Min(GameManager.instance.level, GameManager.instance.nextExp.Length - 1)];
                if (mySlider != null)
                    mySlider.value = curExp / maxExp;
                break;

            case InfoType.Level:
                if (myText != null)
                    myText.text = $"Lv.{GameManager.instance.level}";
                break;

            case InfoType.Kill:
                // DOTween에서 관리하니까 생략 또는 제거
                break;

            case InfoType.Time:
                float remainTime = GameManager.instance.gameTime;
                int min = Mathf.FloorToInt(remainTime / 60);
                int sec = Mathf.FloorToInt(remainTime % 60);
                if (myText != null)
                    myText.text = string.Format("{0:D}:{1:D2}", min, sec);
                break;

            case InfoType.Health:
                float curHealth = GameManager.instance.health;
                float maxHealth = GameManager.instance.maxHealth;
                if (mySlider != null)
                    mySlider.value = curHealth / maxHealth;
                break;
        }
    }
    private void Start()
{
    StartCoroutine(LateStart()); // ← Start에서 코루틴 호출
}

IEnumerator LateStart()
{
    yield return null; // 1프레임 기다림 → GameManager 초기화 기다림

    if (type == InfoType.Kill)
    {
        currentKill = GameManager.instance.kill;
        myText.text = currentKill.ToString();
    }
}
    // Kill 전용 함수 (외부에서 호출)
    public void AnimateKill(int newKill)
    {
        if (type != InfoType.Kill)
            return;

        if (killTween != null && killTween.IsActive())
            killTween.Kill();

        killTween = DOVirtual.Int(currentKill, newKill, 0.3f, value =>
        {
            myText.text = value.ToString();
        });

        currentKill = newKill;

        // 튕김 애니메이션
        transform.DOKill(); // 기존 트윈 제거
        transform.localScale = Vector3.one;
        transform.DOScale(1.2f, 0.1f)
            .SetEase(Ease.OutBack)
            .OnComplete(() => transform.DOScale(1f, 0.1f));
    }
}