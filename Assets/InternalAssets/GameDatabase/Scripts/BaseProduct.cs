using Sirenix.OdinInspector;

using System;

using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "Products", menuName = "GameData/Product", order = 10)]
public class BaseProduct : ScriptableObject
{
    public TypeProducts Type;
    [ListDrawerSettings(ShowIndexLabels = true)]
    public GameGoods[] Goods;
}

[Serializable]
public struct GameGoods
{
    public Sprite Icon;
    public string EditName;
    [HideInInspector] public int Type;
    public int Id;
    public int Price;
    public int Level;
    public int PawnPrice;
    public int BuyBackPrice;
    public LocalizedString Key;
    public GameObject Product;
}