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
        "TIP: ���� ����� ���� ���� �� ���� Ÿ���� �� �־��!",
        "TIP: ���Ÿ� ���⸦ ��� �ʹ� �÷��̸� ���� ��ܺ�����!",
        "TIP: ����� ���������� ���׷��̵�˴ϴ�!",
        "TIP: �־��� �ð����� ������ ��Ƴ����� �¸��մϴ� !",
        "TIP: ü���� 0�� �Ǹ� ���� ����!"
    };
    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }
    void Start()
    {
        // TIP ����
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

            // ���� �ε��� 90% �ѱ� ������ �츮�� ���� ������� ����
            if (fakeProgress < 0.9f)
            {
                fakeProgress += Time.unscaledDeltaTime * 0.3f; // �ӵ� ����
                progressBar.fillAmount = fakeProgress;
            }
            else
            {
                // ���� �ε��� �������� Ȯ��
                if (op.progress >= 0.9f)
                {
                    fakeProgress += Time.unscaledDeltaTime * 0.5f; // �� ���� ������
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
    
