using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GameDataBase : MonoBehaviour
{
    public BaseProduct[] BaseProducts;

    public PawnShopBase Pawnshop;

    [NonReorderable, Space(1)]
    public BaseSensor[] Sensor;

    public TypeProducts UsbRecord;
    public BaseSensor UsbSebsor;

    public static GameDataBase Instance;

    void Awake()
    {
        Instance = this;
    }
}
