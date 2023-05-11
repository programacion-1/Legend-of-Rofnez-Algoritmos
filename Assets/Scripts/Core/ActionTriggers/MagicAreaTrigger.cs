using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class MagicAreaTrigger : MonoBehaviour
    {
        [SerializeField] DamageTrigger _damageTrigger;
        private float _areaDamage;
        private float _lifeSpan;
        private ParticleSystem _particle;
        AudioManager _audioManager;
        [SerializeField] AudioClip _magicClip;
        void Start()
        {
            _audioManager = GameObject.FindObjectOfType<AudioManager>();
            _audioManager.TryToPlayClip(_audioManager.PlayerSFXSources, _magicClip);
            _particle = transform.GetComponentInChildren<ParticleSystem>();
            _lifeSpan = _particle.duration;
            StartCoroutine(DestroyMagic());
        }

        public void SetAreaDamage(float damage)
        {
            _areaDamage = damage;
            _damageTrigger._damageToDeal = _areaDamage;
        }

        private IEnumerator DestroyMagic()
        {
            yield return new WaitForSeconds(_lifeSpan);
            Destroy(gameObject);
        }
    }
}
