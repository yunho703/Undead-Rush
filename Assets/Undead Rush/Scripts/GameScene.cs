using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameScene : MonoBehaviour
{

    public void GameScenesCtrl()
    {
        SceneManager.LoadScene("LoadingScene");
    }
}
