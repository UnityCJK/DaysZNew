using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHit : MonoBehaviour
{
    public BoxCollider boxCollider; //ÁÖ¸Ô ¹üÀ§
    public Transform weaponPointR;
    public GameObject objCol;

    void Start()
    {
        objCol = weaponPointR.GetChild(0).gameObject;
        boxCollider = objCol.GetComponent<BoxCollider>();
    }
    public void WeaponObj()
    {
        objCol = weaponPointR.GetChild(0).gameObject;
        boxCollider = objCol.GetComponent<BoxCollider>();
    }
    public void AttackStart()
    {
        boxCollider.enabled = true;
    }
    public void AttackEnd()
    {
        boxCollider.enabled = false;
    }
}
