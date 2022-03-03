using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public WeaponManager weaponManager;
    public Gun curGun;
    public GameObject player;
    public float curFireRate;
    public bool isActive;
    public bool shotSound =false;
    public Transform bulletPos;
    public GameObject bullet;
    public ObjectManager objectManager;

    private void Update()
    {
        GunFireRate();
    }
    void GunFireRate()
    {
        if(curFireRate > 0)
        {
            curFireRate -= Time.deltaTime;
        }
    }
    public void ShotBtn()
    {
        if (curFireRate <= 0)
            {
            isActive = true;
            Fire();
            }
    }
    public void Fire()
    {
        if (isActive == true)
        {
            isActive = false;
            if (curGun.curBulletCount > 0)
            {
                Shoot();
            }
            else
            {
                StartCoroutine(ReloadCoroutine());
            }
        }
    }
    public void TryReload()
    {
        if (isActive && curGun.curBulletCount < curGun.reloadBulletCount)
        {
            isActive = false;
            StartCoroutine(ReloadCoroutine());
        }
    }
    IEnumerator ReloadCoroutine()
    {
        weaponManager.shotBtn.SetActive(false);
        if(!isActive)
        {
            if (curGun.carryBulletCount > 0)
            {
                player.GetComponent<Animator>().SetTrigger("Reload");
                curGun.carryBulletCount += curGun.curBulletCount;
                curGun.curBulletCount = 0;
                yield return new WaitForSeconds(curGun.reloadTime);
                if (curGun.carryBulletCount >= curGun.reloadBulletCount)
                {
                    curGun.curBulletCount = curGun.reloadBulletCount;
                    curGun.carryBulletCount -= curGun.reloadBulletCount;
                }
                else
                {
                    curGun.curBulletCount = curGun.carryBulletCount;
                    curGun.carryBulletCount = 0;
                }
                yield return new WaitForSeconds(0.5f);
                weaponManager.shotBtn.SetActive(true);
            }
        }

    }
    public void Shoot()
    {
        curGun.gunFlash.Play();
        curGun.gunSound.Play();
        shotSound = true;
        if (curGun.name == "DBSG")
        {
            for (int i = 0; i <= 4; i++)
            {
                bulletPos.transform.Rotate(0, -5+i+3, 0);
                GameObject bullets = objectManager.MakeObj("Bullet");
                bullets.transform.position = bulletPos.transform.position;
                Rigidbody rigids = bullets.GetComponent<Rigidbody>();
                rigids.velocity = bulletPos.forward * 30;
            }
            curGun.curBulletCount--;
            curFireRate = curGun.fireRate;
            isActive = true;
            shotSound = false;
        }
        else
        {
            GameObject bullet = objectManager.MakeObj("Bullet");
            bullet.transform.position = bulletPos.transform.position;
            Rigidbody rigid = bullet.GetComponent<Rigidbody>();
            rigid.velocity = bulletPos.forward * 50;
            curGun.curBulletCount--;
            curFireRate = curGun.fireRate;
            isActive = true;
            shotSound = false;
        }
    }
    public void GunChange(Gun _gun)
    {
        Debug.Log("°ÇÃ¾Áö" + _gun);
        if (WeaponManager.curWeapon != null)
        {
            WeaponManager.curWeapon.gameObject.SetActive(false);
            curGun = _gun;
            WeaponManager.curWeapon = curGun.GetComponent<Transform>();
            ///player.GetComponent<Animator>().SetBool("RifleIdel", true);
            curGun.gameObject.SetActive(true);
        }
        else
        {
            curGun = _gun;
            WeaponManager.curWeapon = curGun.GetComponent<Transform>();
            //player.GetComponent<Animator>().SetBool("RifleIdel", true);
            curGun.gameObject.SetActive(true);
        }
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Ground"))
    //    {
    //        Destroy(gameObject, 3);
    //    }
    //    else if (other.gameObject.CompareTag("Enemy"))
    //    {
    //        Destroy(gameObject, 3);
    //    }
    //}
}



