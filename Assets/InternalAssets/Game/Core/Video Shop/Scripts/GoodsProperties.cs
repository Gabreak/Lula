using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using System;
public class GoodsProperties : MonoBehaviour
{

    #region Cart
    public static List<DataProduct> ProductsBasket;

    public static UnityAction<DataProduct> OnAddToCart { get; set; }
    public static UnityAction<DataProduct> OnFromTheCart { get; set; }

    #endregion

    #region Pawnshop
    public static UnityAction<DataProduct> OnPawn { get; set; }
    public static UnityAction<DataProduct> OnBuyBack { get; set; }
    #endregion

    private void Awake() => ProductsBasket = new List<DataProduct>();


    public static void Buy(DataProduct data) => data.Type.Acquired.Add(data.Goods);

    public static void Remove(DataProduct data) => data.Type.Acquired.Remove(data.Goods);
}

[Serializable]
public struct DataProduct
{
    public GameGoods Goods;
    public TypeProducts Type;
}

