using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Transform player;
    //현재무기와 애니메이션
    public static Transform curWeapon;
    public int curWeaponDamage;

    //무기 타입
    public string curWeaponType;

    //무기 교체
    public static bool isChangeWeapon = false;

    public bool isGun = false;
    //교체 딜레이
    [SerializeField]
    private float changeWeaponDelayTime;
    [SerializeField]
    private float changeWeaponEndDelayTime;
    //무기종류 관리
    [SerializeField]
    private Gun[] guns;
    [SerializeField]
    private MeleeWeapon[] hands;

    public GameObject attackBtn;
    public GameObject shotBtn;

    //HUD
    public HUD hud;
      
    //딕셔너리 관리
    private Dictionary<string, Gun> gunDictionary = new Dictionary<string, Gun>();
    private Dictionary<string, MeleeWeapon> meleeDictionary = new Dictionary<string, MeleeWeapon>();

    [SerializeField]
    private GunController gunController;
    [SerializeField]
    private MeleeAtk meleeAtk;
    void Start()
    {
        for (int i = 0; i < guns.Length; i++)
        {
            gunDictionary.Add(guns[i].gunName, guns[i]);
            //Debug.Log(guns[i] + "건즈");
        }
        for (int i = 0; i < hands.Length; i++)
        {
            meleeDictionary.Add(hands[i].WeaponName, hands[i]);
            //Debug.Log(hands[i] + "핸즈");
        }
    }

    public IEnumerator ChangeWeaponCoroutine(string _type, string _name)
    {
        isChangeWeapon = true;

        yield return new WaitForSeconds(changeWeaponDelayTime);
        CancelAction();
        WeaponChange(_type, _name);
        hud.HUDOnOff();
        yield return new WaitForSeconds(changeWeaponEndDelayTime);
        curWeaponType = _type;
        isChangeWeapon = false;
    }
    private void CancelAction()
    {
        switch(curWeaponType)
        {
            case "Gun":
                gunController.isActive = false;
            break;
            case "Melee":
                meleeAtk.isActive = false;
                break;
        }    
    }
    void WeaponChange(string _type, string _name)
    {
        if (_type == "Gun")
        {
            isGun = true;
            hud.HUDonoff = true;
            shotBtn.SetActive(true);
            attackBtn.SetActive(false);
            if (isGun)
            {
                player.GetComponent<Animator>().SetBool("RifleIdel", true);
                gunController.GunChange(gunDictionary[_name]);
            }
        }
        else if( _type == "Melee")
        {
            isGun = false;
            hud.HUDonoff = false;
            shotBtn.SetActive(false);
            attackBtn.SetActive(true);
            if (isGun == false)
            {
                player.GetComponent<Animator>().SetBool("RifleIdel", false);
                meleeAtk.MeleeChange(meleeDictionary[_name]);
                Debug.Log(curWeapon.GetComponent<MeleeWeapon>().AttackPower);
                //curWeapon.GetComponent<MeleeWeapon>().AttackPower = curWeaponDamage;

            }
        }
    }
}
