using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public int damage;
    public GunController gunController;
    public ZombieAi zombieAi;
    public Gun gun;
    float curTime;
    float desTime = 5f;
    public ParticleSystem muzzleFlash;
    void Start()
    {
        gunController = GameObject.Find("Player").GetComponent<GunController>();
        gun = GetComponent<Gun>();
        zombieAi = GameObject.Find("Zombie1(Clone)").GetComponent<ZombieAi>();
    }
    private void Update()
    {
        GunRange();
        }
    void BulletDmg()
    {
        int bulletDmg = gunController.curGun.damage;
        zombieAi.hp -= bulletDmg;
    }
    void GunRange()
    {
        float gunRange = gunController.curGun.range;
        float dir =  Vector3.Distance(gunController.bulletPos.transform.position, gameObject.transform.position);

        if (dir >= gunRange)
        {
            AutoFalse();
        }
    }
    void AutoFalse()
    {
        gameObject.transform.position = gunController.bulletPos.transform.localPosition;
        Debug.Log(gameObject.transform.position);

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            BulletDmg();
        }
        if(other.CompareTag("Wall"))
        {
            gameObject.SetActive(false);
        }
    }
}



