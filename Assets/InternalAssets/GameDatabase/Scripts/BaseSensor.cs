using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "Sensor", menuName = "GameData/Sensor", order = 60)]
public class BaseSensor : ScriptableObject
{
    public Sprite Error;
    public Sprite OK;
    public TypeProducts Type;
    public LocalizedString Key;
    public GameGoods? Good;
}
