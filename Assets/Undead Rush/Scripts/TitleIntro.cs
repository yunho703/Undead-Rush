using UnityEngine;
using DG.Tweening;
using UnityEngine.UI; // Text
using TMPro; // TextMeshPro ��� �� �ʿ�

public class TitleIntro : MonoBehaviour
{
    public RectTransform title;
    public CanvasGroup titleGroup;

    void Start()
    {
        title.localScale = Vector3.one * 0.6f; // ��¦ ��ҵ� ���¿��� ����
        titleGroup.alpha = 0; // �����ϰ� ����

        title.DOScale(1f, 0.6f)
             .SetEase(Ease.OutBack)
             .SetDelay(0.3f); // 0.3�� �� ����

        titleGroup.DOFade(1f, 0.8f)
             .SetDelay(0.3f);
    }
}