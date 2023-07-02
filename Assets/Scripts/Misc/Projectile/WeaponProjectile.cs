using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponProjectile : Projectile
{
    [Header("Weapon Projectile Stats")]
    //Variable para definir el color del efecto. Principalmente por si es proyectil de player o de enemy
    [SerializeField] TrailRenderer trail;

    public override void ReturnProjectile()
    {
        ArrowProjectileFactory.Instance.ReturnObject(this);
    }

    public void SetProjectileTrail(Color trailColor)
    {
        Gradient trailColorGradient = new Gradient();
        trailColorGradient.SetKeys(new GradientColorKey[]{new GradientColorKey(trailColor, 0.0f) , new GradientColorKey(Color.white, 1.0f)}, new GradientAlphaKey[]{new GradientAlphaKey(1.0f, 1.0f)});
        trail.colorGradient = trailColorGradient;
    }
}
