using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameStartButtonEffect : MonoBehaviour
{
    public RectTransform buttonRect;
    public CanvasGroup buttonGroup;

    void Start()
    {
        // 1. 살짝 축소된 상태에서 시작
        buttonRect.localScale = Vector3.one * 0.7f;
        buttonGroup.alpha = 0f;

        // 2. 등장 애니메이션 (퐁!)
        buttonRect.DOScale(1f, 0.5f)
                  .SetEase(Ease.OutBack)
                  .SetDelay(1f);

        buttonGroup.DOFade(1f, 0.5f)
                   .SetDelay(1f)
                   .OnComplete(() =>
                   {
                       // 3. 반복 깜빡임
                       buttonGroup.DOFade(0.4f, 1f)
                                  .SetLoops(-1, LoopType.Yoyo)
                                  .SetEase(Ease.InOutSine);
                   });
    }
}
