using UnityEngine;
using DG.Tweening;
using UnityEngine.UI; // Text
using TMPro; // TextMeshPro 사용 시 필요

public class TitleIntro : MonoBehaviour
{
    public RectTransform title;
    public CanvasGroup titleGroup;

    void Start()
    {
        title.localScale = Vector3.one * 0.6f; // 살짝 축소된 상태에서 시작
        titleGroup.alpha = 0; // 투명하게 시작

        title.DOScale(1f, 0.6f)
             .SetEase(Ease.OutBack)
             .SetDelay(0.3f); // 0.3초 뒤 등장

        titleGroup.DOFade(1f, 0.8f)
             .SetDelay(0.3f);
    }
}