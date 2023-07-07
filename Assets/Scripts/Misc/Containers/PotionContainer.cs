using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    [SerializeField] Sprite _potionSprite;
    public Sprite potionSprite{get{return _potionSprite;}}
    // Start is called before the first frame update
    void Start()
    {
        _potionName = _potion.name;
    }

}
