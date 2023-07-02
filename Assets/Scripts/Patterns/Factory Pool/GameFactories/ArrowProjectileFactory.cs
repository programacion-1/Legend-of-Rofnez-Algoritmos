using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectileFactory : MonoBehaviour
{
    public static ArrowProjectileFactory Instance { get; private set; }
    [SerializeField] WeaponProjectile _arrowProjectilePrefab;
    [SerializeField] int _initialAmount;
    ObjectPool<WeaponProjectile> _arrowProjectilePool;

    void Awake()
    {
        if (Instance) Destroy(this);
        else Instance = this;

        _arrowProjectilePool = new ObjectPool<WeaponProjectile>(ArrowProjectileCreator, WeaponProjectile.TurnOn, WeaponProjectile.TurnOff, _initialAmount);
    }

    WeaponProjectile ArrowProjectileCreator()
    {
        return Instantiate(_arrowProjectilePrefab, transform);
    }

    public WeaponProjectile GetObject()
    {
        //Pedimos al pool un objeto
        return _arrowProjectilePool.GetObject();
    }

    public void ReturnObject(WeaponProjectile p)
    {
        //Llamamos al pool para devolverle la bala
        _arrowProjectilePool.ReturnObject(p);
    }
}
