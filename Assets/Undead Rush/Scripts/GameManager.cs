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
    public float maxGameTime = 60f; // ���� ������� �ð�
    public float bossTimeLimit = 30f; // ���� ���� �ð�

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

        gameTime = maxGameTime; // ���� �� ���� �ð� ����
        player.gameObject.SetActive(true);
        uiLevelUp.Select(playerId % 2);

        Resume(); // Ÿ�̸� ����
    }

    void Update()
    {
        if (!isLive)
            return;

        // ���� ������ �� �������� ���� �ð� ����
        if (!bossSpawned)
        {
            gameTime -= Time.deltaTime;
            gameTime = Mathf.Clamp(gameTime, 0f, maxGameTime); // 0 �̸� �� �ǰ�

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

        //  ������ AnimatorController ���� ����
        var enemy = boss.GetComponent<Enemy>();
        enemy.anim.runtimeAnimatorController = enemy.animCon[3]; // ���� �ִϸ��̼�

        bossAlive = true;
        Debug.Log("���� ����!");
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