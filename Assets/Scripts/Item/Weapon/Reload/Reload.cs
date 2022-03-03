using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : MonoBehaviour
{
    [SerializeField]
    private Gun gun;
    public void M16Bullet(int _count)
    {
        if (gameObject.name == "M16")
        {
            if (gun.carryBulletCount + _count <= gun.maxBulletCount)
            {
                gun.carryBulletCount += _count;
            }
        }
    }
}
