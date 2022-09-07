using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

public class MoneyProperties : MonoBehaviour
{
    [SerializeField] private int _startMoney = 500;

    [SerializeField] private Text _textMoney;

    [SerializeField] private LocalizedString _messageNoMoneyGirl;
    [SerializeField] private LocalizedString _messageNoMoney;

    private static int s_Money;
    private static Text s_TextMoney;


    private static MoneyProperties _instance;

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
        _instance = this;
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

    public static bool NoMoneyMessage(int price)
    {
        if (price > Money)
        {
            string text = _instance._messageNoMoneyGirl.GetLocalizedString();
            WindowMessage.Message(text, WindowIcon.Warning, Color.yellow);
            return true;
        }
        return false;
    }
    public static void NoMoneyMessage()
    {
            string text = _instance._messageNoMoney.GetLocalizedString();
            WindowMessage.Message(text, WindowIcon.Warning, Color.yellow);

    }

}
