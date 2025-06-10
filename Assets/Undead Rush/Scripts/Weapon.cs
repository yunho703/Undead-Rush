using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;


    public AudioClip shootSound;
    private AudioSource audioSource;

    private float lastShootSoundTime = 0f;
    public float shootSoundCooldown = 0.5f;


    float timer;
    Player player;

    void Awake()
    {
        player = GameManager.instance.player;
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.loop = false;
            audioSource.volume = 0.6f; // 적당한 볼륨
        }
        if (shootSound == null)
        {
            shootSound = Resources.Load<AudioClip>("Fire");
        }

    }

    void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            default:
                timer += Time.deltaTime;

                if (timer > speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;
        }

        /*if (Input.GetButtonDown("Jump"))                Test 코드
        { 
            LevelUp(10, 1);
        } */
    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage * Character.Damage * 0.2f;
        this.count += count;

        this.count = Mathf.Max(2, this.count);

        if (id == 0)
            Batch();

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    public void Init(ItemData data)
    {
        name = "Weapon " + data.itemId;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        id = data.itemId;
        damage = data.baseDamage * Character.Damage;

        count = Mathf.Max(2, data.baseCount + Character.Count);

        for (int index = 0; index < GameManager.instance.pool.prefabs.Length; index++)
        {
            if (data.projectile == GameManager.instance.pool.prefabs[index])
            {
                prefabId = index;
                break;
            }
        }


        switch (id)
        {
            case 0:
                speed = 150 * Character.WeaponSpeed;
                Batch();
                break;
            default:
                speed = 0.3f * Character.WeaponSpeed;
                break;
        }

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    void Batch()
    {
        for (int index = 0; index < count; index++)
        {
            Transform bullet;

            if (index < transform.childCount)
            {
                bullet = transform.GetChild(index);
            }
            else
            {
                bullet = GameManager.instance.pool.Get(prefabId).transform;
                bullet.parent = transform;
            }

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);
            bullet.GetComponent<Bullet>().Init(damage, -100, Vector3.zero); // -100 = 무한관통 공격
        }
    }

    void Fire()
    {
        Debug.Log(" Fire 함수 호출됨");

        if (!player.scanner.nearestTarget)
        {
            Debug.Log(" 타겟이 없음");
            return;
        }
        //  일정 시간마다만 사운드 재생
        if (Time.time - lastShootSoundTime >= shootSoundCooldown)
        {
            audioSource.volume = 0.3f; // 조용하게
            audioSource.PlayOneShot(shootSound);
            lastShootSoundTime = Time.time;
        }

        // 발사 로직은 그대로
        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = (targetPos - transform.position).normalized;
        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;

        bullet.position = transform.position + dir * 0.5f;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, Mathf.Max(1, count), dir);
    }
}
