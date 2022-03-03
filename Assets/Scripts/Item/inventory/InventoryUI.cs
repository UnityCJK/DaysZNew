using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    bool activeInventory = false;
    public GameObject go_SlotParent;
    private Slot[] slots;
    void Start()
    {
        inventoryPanel.SetActive(activeInventory);
        slots = go_SlotParent.GetComponentsInChildren<Slot>(true);
    }
    public void InvenOnoff()
    {
        activeInventory = !activeInventory;
        inventoryPanel.SetActive(activeInventory);
    }
    public void AcquireItem(Item _item, int _count = 1)
    {
        if (Item.ItemType.Equipment != _item.itemType)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    if (slots[i].item.itemName == _item.itemName)
                    {
                        slots[i].SetSlotCount(_count);
                        return;
                    }
                }
            }
        }
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item, _count);
                return;
            }
        }

    }
}

