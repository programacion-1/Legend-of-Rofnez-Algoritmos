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
        public Sprite weaponSprite{get{return _weaponSprite;}}
        protected Transform _rightHand;
        protected Transform _leftHand;
        const string _weaponName = "Weapon";

        public GameObject Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            GameObject newWeapon = null;
            DestroyOldWeapon(rightHand, leftHand);
            
            if(_equippedPrefab != null)
            {
                Transform handTransform = GetTransform(rightHand, leftHand);
                newWeapon = Instantiate(_equippedPrefab, handTransform);
                newWeapon.name = _weaponName;
            }
            if (_animatorOverride != null) animator.runtimeAnimatorController = _animatorOverride;
            return newWeapon;
        }

        public void SetHandTransforms(Transform rightHand, Transform leftHand)
        {
            _rightHand = rightHand;
            _leftHand = leftHand;
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
    }

}