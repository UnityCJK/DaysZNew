using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HUD : MonoBehaviour
{
    [SerializeField]
    private GunController gunController;
    private Gun curGun; 
    //온오프
    [SerializeField]
    private GameObject go_HUD;
    public bool HUDonoff;

    //총알 텍스트
    [SerializeField]
    private Text[] text_Bullet;


    void Start()
    {
        HUDonoff = false;
        go_HUD.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        CheckBullet();
        //HUDOnOff();
    }
    public void HUDOnOff()
    {
        if (HUDonoff)
            go_HUD.SetActive(true);
        if (!HUDonoff)
            go_HUD.SetActive(false);
    }
    private void CheckBullet()
    {
        curGun = gunController.curGun;
        text_Bullet[0].text = curGun.carryBulletCount.ToString();
        text_Bullet[1].text = curGun.reloadBulletCount.ToString();
        text_Bullet[2].text = curGun.curBulletCount.ToString();
    }
}
