using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LoadingSceneController : MonoBehaviour
{
    static string nextScene;

    [SerializeField]
    Image progressBar;

    public Text tipsText;

     [SerializeField]
    string[] tips = {
        "TIP: 근접 무기는 여러 적을 한 번에 타격할 수 있어요!",
        "TIP: 원거리 무기를 골라 초반 플레이를 쉽게 즐겨보세요!",
        "TIP: 무기는 레벨업으로 업그레이드됩니다!",
        "TIP: 주어진 시간동안 적에게 살아남으면 승리합니다 !",
        "TIP: 체력이 0이 되면 게임 오버!"
    };
    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }
    void Start()
    {
        // TIP 설정
        if (tipsText != null && tips.Length > 0)
        {
            tipsText.text = tips[Random.Range(0, tips.Length)];
        }

        StartCoroutine(LoadSceneProcess());
    }

    IEnumerator LoadSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float fakeProgress = 0f;

        while (fakeProgress < 1f)
        {
            yield return null;

            // 실제 로딩이 90% 넘기 전에는 우리가 직접 진행률을 제어
            if (fakeProgress < 0.9f)
            {
                fakeProgress += Time.unscaledDeltaTime * 0.3f; // 속도 조절
                progressBar.fillAmount = fakeProgress;
            }
            else
            {
                // 실제 로딩이 끝났는지 확인
                if (op.progress >= 0.9f)
                {
                    fakeProgress += Time.unscaledDeltaTime * 0.5f; // 더 빨리 차오름
                    progressBar.fillAmount = fakeProgress;

                    if (fakeProgress >= 1f)
                    {
                        op.allowSceneActivation = true;
                        yield break;
                    }
                }
            }
        }
    }
}
    
