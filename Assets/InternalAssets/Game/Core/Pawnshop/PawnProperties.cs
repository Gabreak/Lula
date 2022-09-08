using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class PawnProperties : MonoBehaviour
{
    public RedirectorPawnshop RedirectorPawn;
    public RedirectorPawnshop RedirectorBuyBack;
    public PawnShopBase PawnshopGood;

    private GameObject _selectedPawn;
    private GameObject _selectedBuyBack;

    private Sprite _spriteIcon;

    public DataProduct DataPawn { get; set; }
    public DataProduct DataBuyBack { get; set; }

    public static PawnProperties Instance;

    private void Awake()
    {
        _spriteIcon = RedirectorPawn.Icon.sprite;
        Instance = this;
    }

    private void OnEnable()
    {
        DataPawn = new DataProduct();
        RedirectorPawn.TextPrice.text = "0 $Price";

        DataBuyBack = new DataProduct();
        RedirectorBuyBack.TextPrice.text = "0 $Price";

    }

    public static void ChooseProduct(DataProduct data, GameObject selected)
    {
        Instance.DataPawn = data;
        Instance.RedirectorPawn.Icon.sprite = data.Goods.Icon;
        Instance._selectedPawn = selected;
        Instance.RedirectorPawn.TextPrice.text = data.Goods.PawnPrice + "$ Price";
    }
    public static void ChooseBuyBack(DataProduct data, GameObject selected)
    {
        Instance.DataBuyBack = data;
        Instance.RedirectorBuyBack.Icon.sprite = data.Goods.Icon;
        Instance._selectedBuyBack = selected;
        Instance.RedirectorBuyBack.TextPrice.text = data.Goods.BuyBackPrice + "$ Price";
    }

    public void Pawn()
    {
        Debug.Log(DataPawn.Goods.PawnPrice);
        if (DataPawn.Goods.PawnPrice > 0)
        {
            Destroy(_selectedPawn);
            RedirectorPawn.Icon.sprite = _spriteIcon;

            GoodsProperties.OnPawn(DataPawn);
            RedirectorPawn.TextPrice.text = "0$ Price";
            MoneyProperties.Money += DataPawn.Goods.PawnPrice;

            BaseSensor[] sensors = GameDataBase.Instance.Sensor;
            foreach (var sensor in sensors)
            {
                if (sensor.Good != null)
                    if (sensor.Good.Value.Product == DataPawn.Goods.Product)
                        sensor.Good = null;
            }
            DataPawn = new DataProduct();

        }

    }

    public void BuyBack()
    {
        if (DataBuyBack.Goods.BuyBackPrice > 0 && DataBuyBack.Goods.BuyBackPrice <= MoneyProperties.Money)
        {
            Destroy(_selectedBuyBack);
            RedirectorBuyBack.Icon.sprite = _spriteIcon;

            GoodsProperties.OnBuyBack(DataBuyBack);
            MoneyProperties.Money -= DataBuyBack.Goods.BuyBackPrice;
            RedirectorBuyBack.TextPrice.text = "0$ Price";
            DataBuyBack = new DataProduct();
        }
    }

    public static void InitVisibleGood(RedirectorPawnshop prefab, DataProduct data, Transform parent)
    {
        RedirectorPawnshop redirector = Instantiate(prefab, parent);
        redirector.Data = data;
        redirector.Icon.sprite = data.Goods.Icon;
    }
}
