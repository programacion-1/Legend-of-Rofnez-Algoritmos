using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using GeneralEnums;

namespace RPG.Magic
{
    public abstract class Magic : ScriptableObject
    {
        [SerializeField] float _magicDamage = 5;
        public float magicDamage{get{return _magicDamage;}}
        [SerializeField] float _mpToConsume = 5;
        public float mpToConsume{get{return _mpToConsume;}}
        [SerializeField] float _magicCooldown = 2f;
        public float magicCooldown{get{return _magicCooldown;}}
        [SerializeField] GameObject _equippedPrefab = null;
        public GameObject equippedPrefab{get{return _equippedPrefab;}}
        [SerializeField] Sprite _magicSprite;
        public Sprite magicSprite{get{return _magicSprite;}}
        protected string _magicTypeName;
        public string magicTypeName{get{return _magicTypeName;}}
        protected MagicType _magicType;
        public abstract MagicSpawner InstantiateMagicSpawner(params object[] p);

        public abstract void SetMagicType();

        public void SetAnimatorMagicAnimation(Animator anim, string[] magicAnims)
        {
            for(int i = 0; i < magicAnims.Length; i++)
            {
                if(magicAnims[i] == _magicTypeName) anim.SetBool(magicAnims[i], true);
                else anim.SetBool(magicAnims[i], false);
            }
        }


    }

}