using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

public class MoneyProperties : MonoBehaviour
{
    [SerializeField] private int _startMoney = 500;

    [SerializeField] private Text _textMoney;
    [SerializeField] private TaskClick _task;

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
            _instance._task.Action(s_Money);
        }
    }
    private void Awake()
    {
        _instance = this;
        s_TextMoney = _textMoney;
    }

    private void Start()
    {
        Money = _startMoney;

    }


    //[SerializeField] private int _money = 1000;
    //private void OnGUI()
    //{
    //    if(GUI.Button(new Rect(10, 10, 64, 64), "Money"))
    //    {
    //        Money += _money;
    //    }
    //}

    public static void InfoMoneyBasket(Text info, int price)
    {
        if (Money >= price)
            info.color = new Color32(104, 32, 9, 255);
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
