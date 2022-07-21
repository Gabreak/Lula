using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

public class RedirectorProduct : MonoBehaviour
{
    public DataProduct Data;

    public Text NumberText;
    public Text NameText;
    public Text PriceText;

    public void AddToCart()
    {
        GoodsProperties.OnAddToCart.Invoke(Data);
    }

    public void FromTheCart()
    {
        GoodsProperties.OnFromTheCart.Invoke(Data);
        Destroy(gameObject);
    }
}
