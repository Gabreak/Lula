using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class Thief : MonoBehaviour
{
    [SerializeField] private Text _pricePilice;
    private PoliceManager _police;

    private void Start()
    {
        _police = PoliceManager.Instance;
        int price = 100 * (_police.ToJail + 1) * (_police.WasInJail + 1);
        if (_police.ToJail == 0 && _police.WasInJail == 0)
        {
            price = 0;
        }
        _pricePilice.text = price + "$";
    }

    public void PayPolice()
    {
        if (_police.ToJail == 0 && _police.WasInJail == 0) return;

        int price = 100 * (_police.ToJail + 1) * (_police.WasInJail + 1);
        if (MoneyProperties.Money >= price)
        {
            MoneyProperties.Money -= price;
            _police.ToJail = 0;
            _police.WasInJail = 0;
            _pricePilice.text = 0 + "$";
        }
    }
}
