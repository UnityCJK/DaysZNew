using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M18Reload : MonoBehaviour
{
    [SerializeField]
    private Gun gun;
    public void M18Bullet(int _count)
    {
        if (gameObject.name == "M18")
        {
            if (gun.carryBulletCount + _count <= gun.maxBulletCount)
            {
                gun.carryBulletCount += _count;
            }
        }
    }
}
