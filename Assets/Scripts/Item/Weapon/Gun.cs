using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public string gunName; // 총이름
    public float fireRate;  // 연사속도
    public float reloadTime;  //재장전 속도
    public float range;
    public int damage;  //총 데미지

    public int reloadBulletCount; //재장전수
    public int curBulletCount; //남은총알수
    public int maxBulletCount; // 최대소유가능 총알수
    public int carryBulletCount; // 현재 소유 총알수

    public Animator gunAnim;

    public ParticleSystem gunFlash;
    public AudioSource gunSound;

    public enum Type
    {
        DBSG,
        M16,
        M18
    }
    public void BulletFill()
    {
        GameObject DBSG = GameObject.Find("DBSG");
        GameObject Rifle = GameObject.Find("M16");
    }

    }
