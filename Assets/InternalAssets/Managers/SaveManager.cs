using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SaveManager : MonoBehaviour
{
    string json = "";
    private void OnGUI()
    {
        if (GUI.Button(new Rect(16, 16, 64, 16), "Save"))
        {
            SaveProducts save = new SaveProducts();
            MyGoodsSave(ref save);
            PawnshopSave(ref save);
            json = JsonUtility.ToJson(save);
        }
        if (GUI.Button(new Rect(16, 32, 64, 16), "Load"))
        {
            SaveProducts load = JsonUtility.FromJson<SaveProducts>(json);
            MyGoodsLoad(load);
            PawnshopLoad(load);
        }
    }
    private void MyGoodsSave(ref SaveProducts save)
    {

        save.Money = MoneyProperties.Money;
        BaseProduct[] baseProducts = GameDataBase.Instance.BaseProducts;

        int maxType = baseProducts.Length;

        save.MyData = new SaveGoods[maxType];
        for (int i = 0; i < baseProducts.Length; i++)
        {
            int count = baseProducts[i].Type.Acquired.Count;
            save.MyData[i].TypeIndex = i;
            save.MyData[i].Id = new int[count];
            for (int j = 0; j < count; j++)
            {
                int id = GameDataBase.Instance.BaseProducts[i].Type.Acquired[j].Id;
                save.MyData[i].Id[j] = id;
            }
        }
    }

    private void PawnshopSave(ref SaveProducts save)
    {
        DataProduct[] data = GameDataBase.Instance.Pawnshop.Datas.ToArray();


        save.PawnshopData = new SaveGood[data.Length];
        for (int i = 0; i < data.Length; i++)
        {
            int type = data[i].Type.Index;
            save.PawnshopData[i].TypeIndex = type;
            save.PawnshopData[i].Id = data[i].Goods.Id;
        }
    }

    private void MyGoodsLoad(SaveProducts load)
    {
        BaseProduct[] baseProducts = GameDataBase.Instance.BaseProducts;
        MoneyProperties.Money = load.Money;

        for (int i = 0; i < baseProducts.Length; i++)
        {
            IEnumerable goods = from e in baseProducts[i].Goods
                                from a in load.MyData[i].Id
                                where e.Id == a
                                select e;

            baseProducts[i].Type.Acquired.Clear();

            foreach (GameGoods good in goods)
            {
                baseProducts[i].Type.Acquired.Add(good);
            }
        }
    }

    private void PawnshopLoad(SaveProducts load)
    {
        BaseProduct[] baseProducts = GameDataBase.Instance.BaseProducts;
        GameDataBase.Instance.Pawnshop.Datas.Clear();
        for (int i = 0; i < baseProducts.Length; i++)
        {
            IEnumerable goods = (from a in load.PawnshopData
                                 where baseProducts[i].Type.Index == a.TypeIndex
                                 from e in baseProducts[i].Goods
                                 where a.Id == e.Id
                                 select e);


            foreach (GameGoods good in goods)
            {
                DataProduct goodData;
                goodData.Type = baseProducts[i].Type;
                goodData.Goods = good;
                GameDataBase.Instance.Pawnshop.Datas.Add(goodData);
            }
        }
    }
}

[Serializable]
public class SaveProducts
{
    public int Money;
    public SaveGoods[] MyData;
    public SaveGood[] PawnshopData;

}

[Serializable]
public struct SaveGoods
{
    public int TypeIndex;
    public int[] Id;
}

[Serializable]
public struct SaveGood
{
    public int TypeIndex;
    public int Id;
}
