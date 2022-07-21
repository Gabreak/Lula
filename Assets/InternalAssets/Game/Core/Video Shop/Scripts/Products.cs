using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Products : MonoBehaviour
{
    [SerializeField] private RedirectorProduct _redirector;
    public BaseProduct BaseGoods;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        RedirectorProduct redirector;
        for (int i = 0; i < BaseGoods.Goods.Length; i++)
        {
            redirector = Instantiate(_redirector, transform);
            redirector.NumberText.text = i.ToString();
            redirector.NameText.text = BaseGoods.Goods[i].Key.GetLocalizedString();
            redirector.PriceText.text = BaseGoods.Goods[i].Price.ToString() + "$";
            redirector.Data.Goods = BaseGoods.Goods[i];
            redirector.Data.Type = BaseGoods.Type;
        }
    }

}
