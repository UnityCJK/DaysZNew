using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public ObjectManager objectManager;

    public Transform[] points;

    public GameObject boss;
    public GameObject enemy;
    public float bossRespawnTiem = 10f;
    public int maxBoss = 20;
    public float respawnTime = 1.5f;
    public int maxEnemy = 300;
    public bool isGameOver = false;
    void Start()
    {
        points = GameObject.Find("EnemySpawns").GetComponentsInChildren<Transform>();
        if(points.Length >0)
        {
            StartCoroutine(CallEnemy());
            StartCoroutine(CallBoss());
        }
        //StartEnemy();
    }
    //void StartEnemy()
    //{
    //    for (int i = 0; i < 50; i++)
    //    {
    //        int idx = Random.Range(1, points.Length);
    //        GameObject enemy = objectManager.MakeObj("Enemy");
    //        enemy.transform.position = points[idx].position;
    //    }
    //}
    IEnumerator CallEnemy()
    {
        while(!isGameOver)
        {
            int enemyCount = (int)GameObject.FindGameObjectsWithTag("Enemy").Length;
            if (enemyCount < maxEnemy)
            {
                yield return new WaitForSeconds(respawnTime);
                Debug.Log("좀비생성");
                int idx = Random.Range(1, points.Length);
                GameObject enemy = objectManager.MakeObj("Enemy");
                enemy.transform.position = points[idx].position;
            }

            else yield return null;
        }
    }
    IEnumerator CallBoss()
    {
        while (!isGameOver)
        {
            int bossCount = (int)GameObject.FindGameObjectsWithTag("Boss").Length;
            if (bossCount < maxBoss)
            {
                yield return new WaitForSeconds(bossRespawnTiem);

                int idxx = Random.Range(1, points.Length);
                GameObject boss = objectManager.MakeObj("Boss");
                boss.transform.position = points[idxx].position;
            }

        }

    }
}

