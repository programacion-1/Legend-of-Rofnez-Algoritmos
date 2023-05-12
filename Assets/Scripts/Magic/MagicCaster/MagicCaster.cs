using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

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

        //MagicInventory magicInventory;
        //MagicInventoryMenu magicInventoryMenu;


        public void SetParentMagicCasterStartSettings()
        {
            _anim = GetComponent<Animator>();
            _actionScheduler = GetComponent<ActionScheduler>();
            SetCurrentMagic(_defaultMagic);
            //magicInventory = GetComponent<MagicInventory>();
            //magicInventoryMenu = GameObject.FindObjectOfType<MagicInventoryMenu>();   
        }

        public abstract void ChildSetCurrentMagicSettings();

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
        }

        public void UpdateAnimatorMagicBool()
        {
            if(_currentMagic != null) _currentMagic.SetAnimatorMagicAnimation(_anim, _magicAnims);
        }

        public void MagicAttack()
        {
            if(CheckIfCanUseMagic())
            {
                _anim.ResetTrigger("StopMagicAttack");
                _anim.SetTrigger("MagicAttack");
                _actionScheduler.StartAction(this);
                _timeToActivateMagic = 0f;
            }
        }

        public void ActivateMagic()
        {
            MagicAttackChildSettings();
            _currentMagic.InstantiateMagic(transform, _target);
        }

        protected bool CheckIfCanUseMagic()
        {
            bool firstCondition = MagicAttackChildCondition();
            bool secondCondition = _timeToActivateMagic >= _currentMagic.magicCooldown;
            if(firstCondition && secondCondition) return true;
            else return false;
        }

        public abstract bool MagicAttackChildCondition();
        
        public abstract void MagicAttackChildSettings();

        public void Cancel()
        {
            _anim.ResetTrigger("MagicAttack");
            _anim.SetTrigger("StopMagicAttack");
        }
    }
}
