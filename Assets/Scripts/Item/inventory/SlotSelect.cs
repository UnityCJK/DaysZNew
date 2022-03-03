using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotSelect : MonoBehaviour
{
    [SerializeField]
    private GameObject selBase;

    [SerializeField]
    private Image img;
    [SerializeField]
    private Text txt_ItemName;
    [SerializeField]
    private Text txt_ItemDec;
    [SerializeField]
    private Text txt_useBtn;

    private ItemEffectDB itemEffectDB;
    private Slot slot;

    public void ShowSlot(Item _item)
    {

        selBase.SetActive(true);
        txt_ItemName.text = _item.itemName;
        txt_ItemDec.text = _item.itemDesc;
        img.sprite = _item.itemImage;
        if (_item.itemType == Item.ItemType.Equipment)
            txt_useBtn.text = "ÀåÂø";
        else if (_item.itemType == Item.ItemType.Used)
            txt_useBtn.text = "¸Ô±â";
        else
            txt_useBtn.text = "»ç¿ë";
    }
    public void HideSlot()
    {
        selBase.SetActive(false);
    }
}