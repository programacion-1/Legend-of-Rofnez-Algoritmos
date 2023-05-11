using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _lifeTime;
    float _currentLifeTime;

    void Update()
    {
        transform.position += transform.forward * (_speed * Time.deltaTime);

        _currentLifeTime -= Time.deltaTime;

        if (_currentLifeTime <= 0)
        {
            //"Destruimos" la bala (la devolvemos)
            BulletFactory.Instance.ReturnObject(this);
        }
    }

    //private void OnEnable()
    //{
    //    _currentLifeTime = _lifeTime;
    //}

    //private void OnDisable()
    //{
        
    //}

    private void OnTriggerEnter(Collider other)
    {
        //"Destruimos" la bala (la devolvemos)
        BulletFactory.Instance.ReturnObject(this);
    }

    private void Reset()
    { 
        _currentLifeTime = _lifeTime;
    }

    public static void TurnOn(Bullet b)
    {
        b.Reset();
        b.gameObject.SetActive(true);
    }

    public static void TurnOff(Bullet b)
    {
        b.gameObject.SetActive(false);
    }
}
