using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item",menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public string itemName; // ������ �̸�
    [TextArea]
    public string itemDesc;
    public Sprite itemImage; // �������� �̹���
    public GameObject itemPrefab; // �������� ������
    public string weaponType; //���� ����
    public ItemType itemType;
    public int value;

    public enum ItemType
    {
        Equipment,
        Used,
        Etc
    }
}
