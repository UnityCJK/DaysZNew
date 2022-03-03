using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Slot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private Rect invenRect;

    public Item item; //ȹ���� ������
    public int itemCount; //������ ����
    public Image itemImage; // ������ �̹���.              
    //�ʿ��� ������Ʈ
    [SerializeField]
    private WeaponManager weaponManager;
    [SerializeField]
    private Text text_Count;
    [SerializeField]
    private GameObject go_CountImage;
    private ItemEffectDB itemEffectDB;
    private GameObject selectItem;
    bool isUse = false;

    void Start()
    {
        invenRect = transform.parent.parent.GetComponent<RectTransform>().rect;
        weaponManager = FindObjectOfType<WeaponManager>(); 
        itemEffectDB = FindObjectOfType<ItemEffectDB>();
    }
    // �̹��� ����
    void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }
    //������ ȹ��
    public void AddItem(Item _item, int _Count =1)
    {
        item = _item;
        itemCount = _Count;
        itemImage.sprite = item.itemImage;

        if (item.itemType != Item.ItemType.Equipment)
        {
            go_CountImage.SetActive(true);
            text_Count.text = itemCount.ToString();
        }
        else
        {
            text_Count.text = "0";
            go_CountImage.SetActive(false);
        }

        SetColor(1);
    }
    // ������ ���� ����
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();
        if(itemCount <= 0)
        {
            ClearSlot();
        }
    }
    // ���� �ʱ�ȭ
    public void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        text_Count.text = "0";
        go_CountImage.SetActive(false);
    }
    public void UseItemSlot()
    {
        itemEffectDB.UseItem(item);
        Debug.Log(item.name);
        if (item.itemType == Item.ItemType.Used || item.name == "Grenade")
        {
            ClearSlot();
        }
        isUse = false;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("���");
       
        if (item != null)
        {
            DragSlot.instance.dragSlot = this;
            DragSlot.instance.DragImage(itemImage);
            DragSlot.instance.transform.position = eventData.position;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("�巡��");

        if (item != null)
            DragSlot.instance.transform.position = eventData.position;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (DragSlot.instance.transform.localPosition.x < invenRect.xMin ||
         DragSlot.instance.transform.localPosition.x > invenRect.xMax ||
         DragSlot.instance.transform.localPosition.y < invenRect.yMin ||
         DragSlot.instance.transform.localPosition.y > invenRect.yMax)
        {
            Debug.Log(DragSlot.instance.dragSlot.item.itemPrefab.name);
            if (DragSlot.instance.dragSlot != null && DragSlot.instance.dragSlot.item.itemPrefab.layer == 8
                || DragSlot.instance.dragSlot.item.itemPrefab.layer == 7)
            {
                Instantiate(DragSlot.instance.dragSlot.item.itemPrefab, weaponManager.transform.position, Quaternion.identity);
                DragSlot.instance.dragSlot.ClearSlot();
                DragSlot.instance.SetColor(0);
            }
            else if (DragSlot.instance.dragSlot.item.itemPrefab.name == WeaponManager.curWeapon.name)
            {
                DragSlot.instance.SetColor(0);
            }
        }
        else
        {
            DragSlot.instance.SetColor(0);
            DragSlot.instance.dragSlot = null;
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("�µ��");
        if (DragSlot.instance.dragSlot != null)
        {
            ChangeSlot();
        }
    }
    void ChangeSlot()
    {
        Item _tmItem = item;
        int _tmcount = itemCount;

        AddItem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemCount);
        if(_tmItem !=null)
        {
            DragSlot.instance.dragSlot.AddItem(_tmItem, _tmcount);
        }
        else
        {
            DragSlot.instance.dragSlot.ClearSlot();
        }
    }


}
