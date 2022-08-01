using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GameDataBase : MonoBehaviour
{
    public BaseProduct[] BaseProducts;

    public PawnShopBase Pawnshop;

    [NonReorderable, Space(1)]
    public BaseSensor[] Sensor;
    [Space(20)]
    public BaseRecord UsbRecord;
    [Space(10)]
    public BaseSensor UsbSensor;

    public static GameDataBase Instance;

    void Awake()
    {
        Instance = this;
    }
}
