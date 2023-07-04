using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Namespace general para llamar enums que se utilizarán de forma frecuente en el resto de los scripts
namespace GeneralEnums
{
    public enum MagicType
    {
        Projectile,
        Area,
        Melee,
        Defensive,
        Heal
    }

    public enum CharaType
    {
        Player,
        Enemy
    }

    public enum AttackType
    {
        Weapon,
        Magic,
        Special
    }
}
