using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject bulletPrefab;
    public GameObject bossPrefab;

    public GameObject[] boss;
    public GameObject[] enemy;
    public GameObject[] bullet;

    public GameObject[] ObjectPool;
    //float curTime;
    //float maxTime=3;
    void Awake()
    {
        enemy = new GameObject[350];
        bullet = new GameObject[50];
        boss = new GameObject[30];
        Generate();
    }

    void Start()
    {
        //for (int i = 0; i < 30; i++)
        //{
        //    EnemySpawn();
        //}
    }
    private void Update()
    {
        //curTime += Time.deltaTime;
        //if(curTime >=maxTime)
        //{
        //    EnemySpawn();
        //    curTime = 0;
        //}
    }
    void Generate()
    {
        for (int index = 0; index < enemy.Length; index++)
        {
            enemy[index] = Instantiate(enemyPrefab);
            enemy[index].SetActive(false);
        }
        for (int index = 0; index < bullet.Length; index++)
        {
            bullet[index] = Instantiate(bulletPrefab);
            bullet[index].SetActive(false);
        }
        for (int index = 0; index < boss.Length; index++)
        {
            boss[index] = Instantiate(bossPrefab);
            boss[index].SetActive(false);
        }
    }
    public GameObject MakeObj(string type)
    {

        switch (type)
        {
            case "Enemy":
                ObjectPool = enemy;
                break;

            case "Bullet":
                ObjectPool = bullet;
                break;
            case "Boss":
                ObjectPool = boss;
                break;
            default:
                break;
        }
        for (int index = 0; index < ObjectPool.Length; index++)
        {
            if (!ObjectPool[index].activeSelf)
            {
                ObjectPool[index].SetActive(true);
                return ObjectPool[index];
            }
        }
        return null;
    }

    //void EnemySpawn()
    //{
    //    int ranEnemyA = Random.Range(71, -71);
    //    int ranEnemyB = Random.Range(71, -71);

    //    GameObject enemys = MakeObj("Enemy");
    //    enemys.transform.position = new Vector3(ranEnemyA, 0, ranEnemyB);
    //}
}