using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MyPawnshop : MonoBehaviour
{
    [SerializeField] private RedirectorPawnshop _redirectorPrefab;

    private void Start()
    {
        InitMyProduct();
    }

    private void OnEnable()
    {
        GoodsProperties.OnBuyBack += BuyBack;
    }

    private void OnDisable()
    {
        GoodsProperties.OnBuyBack -= BuyBack;
    }

    private void BuyBack(DataProduct data)
    {
        PawnProperties.Instance.PawnshopGood.Datas.Remove(data);

        GoodsProperties.Buy(data);

        PawnProperties.InitVisibleGood(_redirectorPrefab, data, transform);
    }

    #region On Create
    private void InitMyProduct()
    {
        BaseProduct[] products = GameDataBase.Instance.BaseProducts;

        for (int i = 0; i < products.Length; i++)
        {
            TypeProducts type = products[i].Type;
            AddToSale(type);
        }
    }

    private void AddToSale(TypeProducts type)
    {
        for (int i = 0; i < type.Acquired.Count; i++)
        {
            DataProduct data;
            data.Goods = type.Acquired[i];
            data.Type = type;
            PawnProperties.InitVisibleGood(_redirectorPrefab, data, transform);
        }
    }
    #endregion
}
