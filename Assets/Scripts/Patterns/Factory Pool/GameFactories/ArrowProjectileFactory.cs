using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectileFactory : MonoBehaviour
{
    public static ArrowProjectileFactory Instance { get; private set; }
    [SerializeField] Projectile _arrowProjectilePrefab;
    [SerializeField] int _initialAmount;
    ObjectPool<Projectile> _arrowProjectilePool;

    void Awake()
    {
        if (Instance) Destroy(this);
        else Instance = this;

        _arrowProjectilePool = new ObjectPool<Projectile>(ArrowProjectileCreator, Projectile.TurnOn, Projectile.TurnOff, _initialAmount);
    }

    Projectile ArrowProjectileCreator()
    {
        return Instantiate(_arrowProjectilePrefab, transform);
    }

    public Projectile GetObject()
    {
        //Pedimos al pool un objeto
        return _arrowProjectilePool.GetObject();
    }

    public void ReturnObject(Projectile p)
    {
        //Llamamos al pool para devolverle la bala
        _arrowProjectilePool.ReturnObject(p);
    }
}
