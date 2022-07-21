using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class Cart : MonoBehaviour
{
    [SerializeField] private RedirectorProduct _redrector;

    [SerializeField] private Text _textPrice;
    [SerializeField] private Button _buttonBuy;
    //BaseProduct _baseProduct;
    private int _price;

    private void OnEnable()
    {
        _price = 0;
        MoneyProperties.InfoMoneyBasket(_textPrice, _price);
        GoodsProperties.OnAddToCart += AddToCart;
        GoodsProperties.OnFromTheCart += FromTheCart;
    }

    private void OnDisable()
    {
        GoodsProperties.OnAddToCart -= AddToCart;
        GoodsProperties.OnFromTheCart -= FromTheCart;

        GoodsProperties.ProductsBasket.Clear();
        BasketDestoy();
    }

    private void AddToCart(DataProduct data)
    {

        GoodsProperties.ProductsBasket.Add(data);

        RedirectorProduct redirector;

        redirector = Instantiate(_redrector, transform);
        redirector.NumberText.text = "";//BuyProperties.ProductsBasket.Count.ToString();
        redirector.NameText.text = data.Goods.Key.GetLocalizedString();
        redirector.PriceText.text = data.Goods.Price.ToString() + "$";
        redirector.Data = data;

        _price += data.Goods.Price;

        MoneyProperties.InfoMoneyBasket(_textPrice, _price);
        _buttonBuy.interactable = _price < MoneyProperties.Money;
    }

    private void FromTheCart(DataProduct data)
    {
        _price -= data.Goods.Price;

        GoodsProperties.ProductsBasket.Remove(data);

        MoneyProperties.InfoMoneyBasket(_textPrice, _price);
        _buttonBuy.interactable = _price < MoneyProperties.Money;

    }

    public void BuyAllToCart()
    {
        MoneyProperties.Money -= _price;
        _price = 0;

        MoneyProperties.InfoMoneyBasket(_textPrice, _price);

        for (int i = 0; i < GoodsProperties.ProductsBasket.Count; i++)
        {
            DataProduct data = GoodsProperties.ProductsBasket[i];
            GoodsProperties.Buy(data);
        }

        GoodsProperties.ProductsBasket.Clear();
        StartCoroutine(BuyAll());

    }

    private void BasketDestoy()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    private IEnumerator BuyAll()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
            yield return new WaitForSeconds(0.04f);
        }
        yield return null;
    }

}
