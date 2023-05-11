using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Item;

[RequireComponent(typeof(Potion))]
public class PotionContainer : MonoBehaviour
{
    string _potionName;
    public string potionName{get{return _potionName;}}
    [SerializeField] Potion _potion;
    public Potion potion{get{return _potion;}}
    int _potionQuantity = 0;
    public int potionQuantity{get{return _potionQuantity;} set{_potionQuantity = value;}}
    [SerializeField] int _maxQuantity;
    public int maxQuantity{get{return _maxQuantity;}}
    // Start is called before the first frame update
    void Start()
    {
        _potionName = _potion.name;
    }

}
