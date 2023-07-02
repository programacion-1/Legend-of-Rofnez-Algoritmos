using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballProjectileFactory : MonoBehaviour
{
    public static FireballProjectileFactory Instance { get; private set; }
    [SerializeField] FireballProjectile _fireballProjectilePrefab;
    [SerializeField] int _initialAmount;
    ObjectPool<FireballProjectile> _fireballProjectilePool;

    void Awake()
    {
        if (Instance) Destroy(this);
        else Instance = this;

        _fireballProjectilePool = new ObjectPool<FireballProjectile>(FireballProjectileCreator, FireballProjectile.TurnOn, FireballProjectile.TurnOff, _initialAmount);
    }

    FireballProjectile FireballProjectileCreator()
    {
        return Instantiate(_fireballProjectilePrefab, transform);
    }

    public FireballProjectile GetObject()
    {
        //Pedimos al pool un objeto
        return _fireballProjectilePool.GetObject();
    }

    public void ReturnObject(FireballProjectile p)
    {
        //Llamamos al pool para devolverle la bala
        _fireballProjectilePool.ReturnObject(p);
    }
}
