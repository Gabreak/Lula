using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class MoneyProperties : MonoBehaviour
{

    [SerializeField] private int _startMoney = 500;

    [SerializeField] private Text _textMoney;

    private static int s_Money;
    private static Text s_TextMoney;
    public static int Money
    {
        get => s_Money;
        set
        {
            s_Money = value;
            s_TextMoney.text = s_Money + "$";
        }
    }
    private void Awake()
    {
        s_TextMoney = _textMoney;
        Money = _startMoney;
    }


    public static void InfoMoneyBasket(Text info, int price)
    {
        if (Money >= price)
            info.color = Color.white;
        else
            info.color = Color.red;

        info.text = Money + "$:" + price + "$";
    }

}
