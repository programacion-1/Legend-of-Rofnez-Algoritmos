using System;
using RPG.Core;
using UnityEngine;

namespace RPG.Item
{
    public abstract class Weapon : ScriptableObject
    {
        [SerializeField] protected float _weaponRange;
        public float weaponRange{get{return _weaponRange;}}
        [SerializeField] protected float _weaponDamage;
        public float weaponDamage{get{return _weaponDamage;}}
        [SerializeField] protected float _timeBetweenAttacks;
        public float timeBetweenAttacks{get{return _timeBetweenAttacks;}}
        [SerializeField] bool isRightHanded;
        [SerializeField] protected GameObject _equippedPrefab = null;
        public GameObject equippedPrefab{get{return _equippedPrefab;}}
        [SerializeField] AnimatorOverrideController _animatorOverride = null;
        [SerializeField] Sprite _weaponSprite;
        protected Transform _rightHand;
        protected Transform _leftHand;
        const string _weaponName = "Weapon";

        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            DestroyOldWeapon(rightHand, leftHand);
            
            if(_equippedPrefab != null)
            {
                _rightHand = rightHand;
                _leftHand = leftHand;
                Transform handTransform = GetTransform(_rightHand, _leftHand);
                GameObject newWeapon = Instantiate(_equippedPrefab, handTransform);
                newWeapon.name = _weaponName;
                SpawnSettings(newWeapon);
            }
            if (_animatorOverride != null) animator.runtimeAnimatorController = _animatorOverride;
        }

        public Transform GetTransform(Transform rightHand, Transform leftHand)
        {
            Transform handTransform;
            if (isRightHanded) handTransform = rightHand;
            else handTransform = leftHand;
            return handTransform;
        }

        private void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
            Transform oldWeapon = rightHand.Find(_weaponName);
            if(oldWeapon == null) oldWeapon = leftHand.Find(_weaponName);
            if(oldWeapon == null) return;

            oldWeapon.name = "DESTROYING";
            Destroy(oldWeapon.gameObject);
        }

        public abstract void SetEquippedWeaponDamage();
        public abstract void SpawnSettings(GameObject spawnedWeapon);
        public abstract void WeaponAttack(Health t);
        public abstract void StopAttack();
    }

}