using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    string json = "";

    public static SaveManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        int index = ScenesManager.SceneOpen;
        if (index == 0)
            DeleteSave(false);
        else if (index == 1)
            LoadGame();
    }

    private void OnDisable()
    {
        SaveGame();
    }

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("Goods"))
        {
            json = PlayerPrefs.GetString("Goods");

            SaveProducts load = JsonUtility.FromJson<SaveProducts>(json);
            MoneyProperties.Money = load.Money;
            Debug.Log(load.Money);

            MyGoodsLoad(load);
            PawnshopLoad(load);
            MyRoomLoad(load);
            loadRecordVideo(load);
            LoadDoor(load);
        }
    }

    public void SaveGame()
    {
        SaveProducts save = new SaveProducts();
        save.Money = MoneyProperties.Money;
        
        MyGoodsSave(ref save);
        PawnshopSave(ref save);
        MyRoomSave(ref save);
        SaveRecordVideo(ref save);
        SaveDoorRoom(ref save);

        json = JsonUtility.ToJson(save);

        PlayerPrefs.SetString("Goods", json);
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(16, 16, 64, 16), "Save"))
        {
            SaveGame();
        }
        if (GUI.Button(new Rect(16, 32, 64, 16), "Load"))
        {
            LoadGame();
        }
        if (GUI.Button(new Rect(16, 48, 64, 16), "Clear"))
        {
            DeleteSave();
        }

    }

    public void DeleteSave(bool reset = true)
    {
        PlayerPrefs.DeleteAll();
        GameDataBase data = GameDataBase.Instance;
        foreach (BaseProduct good in data.BaseProducts)
        {
            good.Type.Acquired.Clear();
        }
        data.Pawnshop.Datas.Clear();

        PoliceManager.Instance.ToJail = 0;
        PoliceManager.Instance.WasInJail = 0;
        if (reset)
            SceneManager.LoadScene(0);
    }

    private void MyGoodsSave(ref SaveProducts save)
    {

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

    private void MyRoomSave(ref SaveProducts save)
    {
        BaseSensor[] data = GameDataBase.Instance.Sensor.ToArray();

        save.SensorGood = new SaveGood[data.Length];

        for (int i = 0; i < data.Length; i++)
        {

            int typeIndex = data[i].Type.Index;
            save.SensorGood[i].TypeIndex = typeIndex;
            save.SensorGood[i].Id = (data[i].Good != null) ? data[i].Good.Value.Id : -1;
        }
    }

    private void SaveRecordVideo(ref SaveProducts save)
    {
        save.VideoData = GameDataBase.Instance.UsbRecord.Records.ToArray();
    }

    private void SaveDoorRoom(ref SaveProducts save)
    {

        save.SaveRoomDoor.Hour = DoorManager.Instance.Hour;
        save.SaveRoomDoor.IsDoor = new bool[DoorManager.Instance.isDoor.Length];
        save.SaveRoomDoor.IsDoor = DoorManager.Instance.isDoor;

    }

    private void MyGoodsLoad(SaveProducts load)
    {
        BaseProduct[] baseProducts = GameDataBase.Instance.BaseProducts;

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

    private void MyRoomLoad(SaveProducts load)
    {

        //BaseProduct[] baseProducts = GameDataBase.Instance.BaseProducts;
        BaseSensor[] sensor = GameDataBase.Instance.Sensor;

        for (int i = 0; i < sensor.Length; i++)
        {
            sensor[i].Good = null;
        }

        for (int i = 0; i < sensor.Length; i++)
        {

            var goods = (from a in load.SensorGood
                         where sensor[i].Type.Index == a.TypeIndex
                         from e in sensor[i].Type.Acquired
                         where a.Id == e.Id
                         select e);

            foreach (GameGoods good in goods)
            {
                sensor[i].Good = good;
            }
        }
    }

    private void loadRecordVideo(SaveProducts load)
    {
        BaseRecord record = GameDataBase.Instance.UsbRecord;
        record.Records = load.VideoData.ToList();
    }

    private void LoadDoor(SaveProducts load)
    {
        DoorManager.Instance.Hour = load.SaveRoomDoor.Hour;
        DoorManager.Instance.isDoor = load.SaveRoomDoor.IsDoor;
    }
}

[Serializable]
public class SaveProducts
{
    public int Money;
    public SaveGoods[] MyData;
    public SaveGood[] PawnshopData;
    public SaveGood[] SensorGood;
    public VideoRecord[] VideoData;
    public SaveDoor SaveRoomDoor;
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

[Serializable]
public struct SaveDoor
{
    public int Hour;
    public bool[] IsDoor;
}
