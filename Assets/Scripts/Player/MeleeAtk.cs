using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MeleeAtk : MonoBehaviour
{
    MultiJoy multiJoy;
    public Transform player;
    public Transform grenadePos;
    public MeleeWeapon curHand;
    public ObjectManager objectManager;
    public Slot slot;
    public bool isAttack = false;
    public bool isActive;
    private float curAttackTime;
    private float AttackCoolTime = 1.5f;
    public Grenade grenadeclaas;
    public GameObject grenadeObj;
    public bool isGrenade = false;

    private void Start()
    {
        multiJoy = FindObjectOfType<MultiJoy>();
        curHand = GetComponent<MeleeWeapon>();
        curAttackTime = 0f;
        
    }
    private void Update()
    {
        //Debug.Log(curHand);
        curAttackTime += Time.deltaTime;
        if(curAttackTime >= AttackCoolTime)
        {
            curAttackTime = 0;
            isAttack = true;
        }
    }
    void Grenade()
    {
        GameObject grenade = Instantiate(grenadeObj, player.transform.position, player.transform.rotation);
        grenade.transform.position = grenadePos.transform.position;
        Rigidbody rigid = grenade.GetComponent<Rigidbody>();
        rigid.isKinematic = false;
        rigid.AddForce(grenade.transform.forward * 70, ForceMode.Impulse);
        grenade.transform.GetComponent<Grenade>().enabled = true;
        WeaponManager.curWeapon.gameObject.SetActive(false);
        WeaponManager.curWeapon = null;
    }    

    public void MeleeAttack()
    {
        //Debug.Log(curHand.AttackPower);
        if (isAttack)
        {
            player.GetComponent<Animator>().SetTrigger("attack");

            if (WeaponManager.curWeapon.name == "Grenade")
            {
                Grenade();
            }
        }
    }
    public void MeleeChange(MeleeWeapon _hand)
    {
        
        if (WeaponManager.curWeapon != null)
        {
            WeaponManager.curWeapon.gameObject.SetActive(false);
            curHand = _hand;
            //Debug.Log(curHand);
            WeaponManager.curWeapon = curHand.GetComponent<Transform>();
            curHand.gameObject.SetActive(true);
            curHand.gameObject.transform.SetAsFirstSibling();
            Debug.Log(WeaponManager.curWeapon);
        }
        else
        {
            curHand = _hand;
           //Debug.Log(curHand);
           //Debug.Log(_hand);
            WeaponManager.curWeapon = curHand.GetComponent<Transform>();
            curHand.gameObject.SetActive(true);
            curHand.gameObject.transform.SetAsFirstSibling();
        }
    }

}
