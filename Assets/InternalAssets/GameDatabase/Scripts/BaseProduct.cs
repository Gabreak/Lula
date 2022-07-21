using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "Products", menuName = "GameData/Product", order = 10)]
public class BaseProduct : ScriptableObject
{
    public TypeProducts Type;
    public GameGoods[] Goods;
}

[Serializable]
public struct GameGoods
{
    public Sprite Icon;
    [HideInInspector] public int Type;
    public int Id;
    public int Price;
    public int Level;
    public int PawnPrice;
    public int BuyBackPrice;
    public LocalizedString Key;
    public GameObject Product;
}