using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Game Control")]
    public bool isLive;
    public float gameTime;
    public float maxGameTime = 60f; // 보스 등장까지 시간
    public float bossTimeLimit = 30f; // 보스 제한 시간

    [Header("# Player Info")]
    public int playerId;
    public float health;
    public float maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp;

    [Header("# Game Object")]
    public PoolManager pool;
    public Player player;
    public LevelUp uiLevelUp;
    public Result uiResult;
    public GameObject enemyCleaner;
    public HUD killHUD;

    [Header("# Boss")]
    public GameObject bossPrefab;
    public Transform bossSpawnPoint;

    public bool bossSpawned = false;
    private bool bossAlive = false;

    void Awake()
    {
        instance = this;
    }

    public void GameStart(int id)
    {
        playerId = id;
        health = maxHealth;

        gameTime = maxGameTime; // 시작 시 남은 시간 설정
        player.gameObject.SetActive(true);
        uiLevelUp.Select(playerId % 2);

        Resume(); // 타이머 시작
    }

    void Update()
    {
        if (!isLive)
            return;

        // 아직 보스가 안 나왔으면 게임 시간 감소
        if (!bossSpawned)
        {
            gameTime -= Time.deltaTime;
            gameTime = Mathf.Clamp(gameTime, 0f, maxGameTime); // 0 미만 안 되게

            if (gameTime <= 0f)
            {
                gameTime = 0f;
                bossSpawned = true;
                SpawnBoss();
            }
        }
        else if (bossAlive)
        {
            bossTimeLimit -= Time.deltaTime;

            if (bossTimeLimit <= 0f)
            {
                GameOver();
            }
        }
    }

    void SpawnBoss()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 bossPos = playerPos + new Vector3(7f, 4f, 0);

        GameObject boss = Instantiate(bossPrefab, bossPos, Quaternion.identity);

        //  보스의 AnimatorController 직접 연결
        var enemy = boss.GetComponent<Enemy>();
        enemy.anim.runtimeAnimatorController = enemy.animCon[3]; // 보스 애니메이션

        bossAlive = true;
        Debug.Log("보스 등장!");
    }

    public void OnBossDefeated()
    {
        if (!bossAlive) return;

        bossAlive = false;
        GameVictory();
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        isLive = false;
        yield return new WaitForSeconds(0.5f);
        uiResult.gameObject.SetActive(true);
        uiResult.Lose();
        Stop();
    }

    public void GameVictory()
    {
        StartCoroutine(GameVictoryRoutine());
    }

    IEnumerator GameVictoryRoutine()
    {
        isLive = false;
        enemyCleaner.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        uiResult.gameObject.SetActive(true);
        uiResult.Win();
        Stop();
    }
    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }
    public void AddKill()
    {
        kill++;
        killHUD.AnimateKill(kill);
    }

    public void GetExp()
    {
        if (!isLive)
            return;

        exp++;

        if (exp == nextExp[Mathf.Min(level, nextExp.Length - 1)])
        {
            level++;
            exp = 0;
            uiLevelUp.Show();
        }
    }

    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
    }
}