using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public string WeaponName; // 무기 이름
    public int AttackPower; // 공격력
    public float AttackRange; // 공력 사정거리
    public float Durable; // 내구도
    public float DropProbability; //드랍 확률
    public int DropOffLocation; // 드랍 장소

    public float attackDelay; // 공격 딜레이

    public Animator meleeAnim;  // 애니메이션

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
