using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using GeneralEnums;

namespace RPG.Magic
{
    public abstract class MagicCaster : MonoBehaviour, IAction
    {
        [SerializeField] Transform _rightHandTransform;
        [SerializeField] Transform _leftHandTransform;
        Animator _anim;
        ActionScheduler _actionScheduler;
        [SerializeField] string[] _magicAnims;
        [SerializeField] Magic _defaultMagic = null;
        [SerializeField] protected Magic _currentMagic;
        public Magic currentMagic{get{return _currentMagic;}}
        float _timeToActivateMagic = Mathf.Infinity;
        private Health _target;
        public Health target{get{return _target;} set{_target = value;}}
        [SerializeField] protected MagicSpawner _magicSpawner;
        Dictionary<string, Transform> _transformsByMagicType;

        //MagicInventory magicInventory;
        //MagicInventoryMenu magicInventoryMenu;


        public void SetParentMagicCasterStartSettings()
        {
            _anim = GetComponent<Animator>();
            _actionScheduler = GetComponent<ActionScheduler>();
            SetTransformsByMagicType();
            SetCurrentMagic(_defaultMagic);
            //magicInventory = GetComponent<MagicInventory>();
            //magicInventoryMenu = GameObject.FindObjectOfType<MagicInventoryMenu>();   
        }

        public void SetTransformsByMagicType()
        {
            _transformsByMagicType = new Dictionary<string, Transform>();
            _transformsByMagicType.Add(MagicType.Area.ToString(), transform);
            _transformsByMagicType.Add(MagicType.Projectile.ToString(), _rightHandTransform);
        }

        public virtual void ChildSetCurrentMagicSettings()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if(_currentMagic == null) return;
            _timeToActivateMagic += Time.deltaTime; 
        }

        public void SetCurrentMagic(Magic magic)
        {
            if(magic == null) return;
            _currentMagic = magic;
            _currentMagic.SetMagicType();
            UpdateAnimatorMagicBool();
            ChildSetCurrentMagicSettings();
            SetMagicSpawnerType(_currentMagic, _currentMagic.magicTypeName);
        }

        public void SetMagicSpawnerType(Magic magic, string magicType)
        {
            if(_transformsByMagicType.ContainsKey(magicType))
            {
                _magicSpawner = magic.InstantiateMagicSpawner(_transformsByMagicType[magicType]);
                _magicSpawner.EquipMagicOnSpawner(magic);
                _magicSpawner.SetSpawnerTransform(_transformsByMagicType[magicType]);
            } 
        }

        public void UpdateAnimatorMagicBool()
        {
            if(_currentMagic != null) _currentMagic.SetAnimatorMagicAnimation(_anim, _magicAnims);
        }

        public void MagicAttack()
        {
            if(CheckIfCanUseMagic())
            {
                MagicAttackChildSettings();
                UpdateAnimatorMagicBool();
                _actionScheduler.StartAction(this);
                _anim.ResetTrigger("StopMagicAttack");
                _anim.SetTrigger("MagicAttack");
            }
        }

        public void ActivateMagic()
        {
            _magicSpawner.CastMagic(_target, gameObject.layer);
            ActionsAfterActivatingMagic();
        }

        public virtual void ActionsAfterActivatingMagic()
        {
            _timeToActivateMagic = 0f;
        }

        protected bool CheckIfCanUseMagic()
        {
            bool firstCondition = MagicAttackChildCondition();
            bool secondCondition = _timeToActivateMagic >= _currentMagic.magicCooldown;
            if(firstCondition && secondCondition) return true;
            else return false;
        }

        public virtual bool MagicAttackChildCondition()
        {
            return true;
        }
        
        public virtual void MagicAttackChildSettings()
        {

        }

        public void Cancel()
        {
            _anim.ResetTrigger("MagicAttack");
            _anim.SetTrigger("StopMagicAttack");
        }
    }
}
