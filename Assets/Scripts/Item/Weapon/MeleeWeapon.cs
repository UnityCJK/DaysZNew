using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public string WeaponName; // ���� �̸�
    public int AttackPower; // ���ݷ�
    public float AttackRange; // ���� �����Ÿ�
    public float Durable; // ������
    public float DropProbability; //��� Ȯ��
    public int DropOffLocation; // ��� ���

    public float attackDelay; // ���� ������

    public Animator meleeAnim;  // �ִϸ��̼�

    public enum Type {
    BaseballBat,
    Guitar,
    CrowBar,
    Shovel,
    FryingPan,
    Hammer,
    IronPipe,
    FireAxe,
    PickAxe,
    KitchenKnife,
    HuntingKnife,
    Clipper,
    Driver,
    Fork,
    Pen,
    Punch

    }

}
