using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.Combat;

public abstract class Projectile : MonoBehaviour
{
    //Objetivo del proyectil
    [SerializeField] Health _target;
    //Trigger donde se setea el daño del proyectil
    DamageTrigger _damageTrigger;
    [SerializeField] float _speed;
    //Bool para chequear si es teledirigido
    [SerializeField] private bool _isHoming;
    public float _damage{get; set;}
    [SerializeField] GameObject _hitEffect = null;
    //Vida útil del objetio
    [SerializeField] float _lifeSpan = 1f;
    float _lifeTime = 0f;
    //Lista de objectos (layers) con los que puedo impactar
    [SerializeField] List<int> _impactObjects = new List<int>();
    [Header("Audio Clips")]
    public AudioClip _impactClip;
    public AudioClip _launchClip;
    [SerializeField] AudioManager _audioManager;
    void Start()
    {
        Reset();
        #region testing
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        if(_target == null) return;
        if(_isHoming && !_target.CheckIfIsDead()) transform.LookAt(GetAimLocation());
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    public void SetProjectileDamage(float damage)
    {
        _damage = damage;
        _damageTrigger._damageToDeal = _damage;
    }

    public void SetProjectileTarget(Health t, int impactObjectLayer)
    {
        _target = t;
        if(_damageTrigger == null) _damageTrigger = GetComponent<DamageTrigger>();
        _damageTrigger.SetDamageTarget(_target);
        TryAddImpactObject(impactObjectLayer);
        transform.LookAt(GetAimLocation());
    }

    //Funcion para evitar que se repitan impactObjects
    public void TryAddImpactObject(int objectLayer)
    {
        for(int i = 0; i < _impactObjects.Count; i++)
        {
            if(_impactObjects[i] != objectLayer)
            {
                _impactObjects.Add(objectLayer);
                break;
            }
        }
    }

    //Clase para buscar el objetivo a apuntar
    private Vector3 GetAimLocation()
    {
        if(_target == null) return transform.position;        
        if(_target.GetComponent<CapsuleCollider>() == null) return _target.transform.position;
        Vector3 AimLocation = _target.transform.position + Vector3.up * _target.GetComponent<CapsuleCollider>().height / 2;
        return AimLocation;
    }

    private bool ImpactEffect()
    {
        _audioManager.TryToPlayClip(_audioManager.obstacleSources, _impactClip);
        if(_hitEffect != null)
        {
            Instantiate(_hitEffect, GetAimLocation(), transform.rotation);
        }
        return true;
    }  

    private IEnumerator DestroyProjectileByLifeSpanCo()
    {
        yield return new WaitForSeconds(_lifeSpan);
        ReturnProjectile();
    }

    public abstract void ReturnProjectile();

    public void DestroyProjectileByLifeSpan()
    {
        StartCoroutine("DestroyProjectileByLifeSpanCo");
    }

    private void DestroyProjectileByImpact()
    {
        StopCoroutine(DestroyProjectileByLifeSpanCo());
        ReturnProjectile();
        //Destroy(gameObject);     
    }

    private void OnTriggerEnter(Collider other)
    {
        bool canBeDestroyed = false;
        for(int i = 0; i < _impactObjects.Count; i++)
        {
            if(other.gameObject.layer == _impactObjects[i])
            {
                canBeDestroyed = ImpactEffect();
                break;
            }
        }
        if(canBeDestroyed) DestroyProjectileByImpact();
    }

    private void Reset()
    { 
        _damageTrigger = GetComponent<DamageTrigger>();
        _audioManager = GameObject.FindObjectOfType<AudioManager>();
        _audioManager.TryToPlayClip(_audioManager.trapSources, _launchClip);
        transform.LookAt(GetAimLocation());
    }

    public static void TurnOn(Projectile p)
    {
        p.gameObject.SetActive(true);
        p.Reset();
        p.DestroyProjectileByLifeSpan();
    }

    public static void TurnOff(Projectile p)
    {
        p.gameObject.SetActive(false);
    }
}
