using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;


[CreateAssetMenu(fileName = "Character", menuName = "GameData/Character", order = 200)]
public class CharacterDataBase : ScriptableObject
{
    public string Name;
    public bool IsWorking;
    public int IndexTime;
    public TimeWorking[] Working;
}

[Serializable]
public struct TimeWorking
{
    public int Begin;
    public int End;
    public int Profit;
}
