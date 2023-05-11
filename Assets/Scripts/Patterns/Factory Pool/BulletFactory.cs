using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : MonoBehaviour
{
    public static BulletFactory Instance { get; private set; }

    [SerializeField] Bullet _bulletPrefab;
    [SerializeField] int _initialAmount;

    ObjectPool<Bullet> _bulletPool;

    void Awake()
    {
        if (Instance) Destroy(this);
        else Instance = this;

        _bulletPool = new ObjectPool<Bullet>(BulletCreator, Bullet.TurnOn, Bullet.TurnOff, _initialAmount);

        /* LAMBDAS
        _bulletPool = new ObjectPool<Bullet>(() => Instantiate(_bulletPrefab, transform),
                                             (b) => b.gameObject.SetActive(true),
                                             (b) => b.gameObject.SetActive(false),
                                             _initialAmount);
        */
    }


    Bullet BulletCreator()
    {
        return Instantiate(_bulletPrefab, transform);
    }

    public Bullet GetObject()
    {
        //Pedimos al pool un objeto
        return _bulletPool.GetObject();
    }

    public void ReturnObject(Bullet b)
    {
        //Llamamos al pool para devolverle la bala
        _bulletPool.ReturnObject(b);
    }
    
}
