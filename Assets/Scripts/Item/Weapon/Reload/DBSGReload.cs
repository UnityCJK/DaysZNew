using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBSGReload : MonoBehaviour
{
    [SerializeField]
    private Gun gun;
    public void DBSGBullet(int _count)
    {
        Debug.Log("¼¦°Ç");
        if (gameObject.name == "DBSG")
        {
            if (gun.carryBulletCount + _count <= gun.maxBulletCount)
            {
                gun.carryBulletCount += _count;
            }
        }
    }
}
