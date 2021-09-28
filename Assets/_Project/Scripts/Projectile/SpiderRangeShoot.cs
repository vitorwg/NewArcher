using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderRangeShoot : SystemShootProjectile
{
    private void OnTriggerEnter(Collider other)
    {
        //animator
        OnShoot();
    }
}
