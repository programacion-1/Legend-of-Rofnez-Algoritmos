using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Item
{
    public abstract class Potion : MonoBehaviour
    {
        [SerializeField] float potionValue;
        [SerializeField] GameObject potionVFX;

    }
}