using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemEffcet
{
    public string itemName; // 키값
    public string[] part; // 
    public int[] num; // 수치
}
public class ItemEffectDB : MonoBehaviour
{
    [SerializeField]
    private StatusController playerStatus;
    [SerializeField]  
    private WeaponManager weaponManager;
    [SerializeField]
    private SlotSelect slotSelect;
    [SerializeField]
    private Slot slot;
    [SerializeField]
    private Reload reload;
    [SerializeField]
    private M18Reload m18Reload;
    [SerializeField]
    private DBSGReload dBSGReload;
    [SerializeField]
    private ItemEffcet[] itemEffects;
    private const string HP = "HP", SP = "SP", HUNGRY = "HUNGRY", THIRSTY = "THIRSTY", FATIGUE = "FATIGUE", DBSG = "DBSG", M16 = "M16", M18 = "M18";


    public void UseItem(Item _item)
    {
        if (_item.itemType == Item.ItemType.Equipment)
        {
            StartCoroutine(weaponManager.ChangeWeaponCoroutine(_item.weaponType, _item.itemName));
            if (_item.name == "Grenade")
            {
                slot.ClearSlot();
            }
        }
        else if (_item.itemType == Item.ItemType.Used)
        {
            //Debug.Log(_item.itemName);
            
            for (int x = 0; x < itemEffects.Length; x++)
            {
                if(itemEffects[x].itemName == _item.itemName)
                {
                    for (int y = 0; y < itemEffects[x].part.Length; y++)
                    {
                        switch (itemEffects[x].part[y])
                        {
                            case HP:
                                playerStatus.HpPlus(itemEffects[x].num[y]);
                                break;
                            case HUNGRY:
                                playerStatus.HungryPlus(itemEffects[x].num[y]);
                                break;
                            case THIRSTY:
                                playerStatus.ThirstyPlus(itemEffects[x].num[y]);
                                break;
                            case FATIGUE:
                                playerStatus.FatiguePlus(itemEffects[x].num[y]);
                                break;
                            case DBSG:
                                dBSGReload.DBSGBullet(itemEffects[x].num[y]);
                                break;
                            case M16:
                                reload.M16Bullet(itemEffects[x].num[y]);
                                break;
                            case M18:
                                m18Reload.M18Bullet(itemEffects[x].num[y]);
                                break;
                            default:
                                //Debug.Log("잘못된 부위입니다.");
                                break;
                        }
                        //Debug.Log(_item + "을 사용");
                    }
                    return;
                }
            }
            //Debug.Log("일치하는 아이템이 없습니다.");
        }
    }

}
