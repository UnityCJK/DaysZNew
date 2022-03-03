using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Transform player;
    //���繫��� �ִϸ��̼�
    public static Transform curWeapon;
    public int curWeaponDamage;

    //���� Ÿ��
    public string curWeaponType;

    //���� ��ü
    public static bool isChangeWeapon = false;

    public bool isGun = false;
    //��ü ������
    [SerializeField]
    private float changeWeaponDelayTime;
    [SerializeField]
    private float changeWeaponEndDelayTime;
    //�������� ����
    [SerializeField]
    private Gun[] guns;
    [SerializeField]
    private MeleeWeapon[] hands;

    public GameObject attackBtn;
    public GameObject shotBtn;

    //HUD
    public HUD hud;
      
    //��ųʸ� ����
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
            //Debug.Log(guns[i] + "����");
        }
        for (int i = 0; i < hands.Length; i++)
        {
            meleeDictionary.Add(hands[i].WeaponName, hands[i]);
            //Debug.Log(hands[i] + "����");
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
