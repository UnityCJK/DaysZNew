using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemGet : MonoBehaviour
{
    public Transform player;
    public bool isdown;
    public GameObject[] weapons;
    public bool[] hasWeapon;
    GameObject nearObject;

    //아이템에만 반응하도록 설정
    [SerializeField]
    private InventoryUI theInventory;
    private Slot slot;
    private MultiJoy multiJoy;
    private GameObject statusController;
    private Animator playerAnim;
    public GameObject UsingEffect;

    private void Start()
    {
        multiJoy = FindObjectOfType<MultiJoy>();
        statusController = GameObject.Find("Status");
        playerAnim = GetComponent<Animator>();
    }
    public void Pdown()
    {
        if (isdown == true)
        {
            UsingEffect.SetActive(false);
            IsStopON();
            player.GetComponent<Animator>().SetTrigger("Down");
            Interration();
            Invoke("IsStopOff", 0.8f);
        }
    }
    public void Interration()
    {
        if (isdown = true && nearObject != null)
        {
            if (nearObject.tag == "Item")
            {
                Debug.Log(nearObject.transform.GetComponent<ItemPickup>().item + "획득함");
                theInventory.AcquireItem(nearObject.transform.GetComponent<ItemPickup>().item);
                isdown = false;
                Destroy(nearObject);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            UsingEffect.SetActive(true);
            Debug.Log(other);
            isdown = true;
            nearObject = other.gameObject;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyAttack"))
        {
            StartCoroutine(PlayerDamage());
        }
    }
    IEnumerator PlayerDamage()
    {
        IsStopON();
        playerAnim.SetTrigger("Damage");
        statusController.GetComponent<StatusController>().HpPlus(Random.Range(-5, -15));
        yield return new WaitForSeconds(0.8f);
        IsStopOff();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Item")
        {
            UsingEffect.SetActive(false);
            isdown = false;
            nearObject = null;
        }
    }
    void IsStopON()
    {
        multiJoy.isStop = true;
        isdown = false;
    }
    void IsStopOff()
    {
        multiJoy.isStop = false;
    }
}
