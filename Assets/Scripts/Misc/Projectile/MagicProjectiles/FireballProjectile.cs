using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballProjectile : MagicProjectile
{
    public override void ReturnProjectile()
    {
       FireballProjectileFactory.Instance.ReturnObject(this);
    }

}
