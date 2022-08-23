using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "Girls", menuName = "GameData/Girl", order = 270)]
public class GirlsData : ScriptableObject
{
    
}


public struct GirlBase
{
    public bool IsActive;
    public string Name;
    public int Id;
    public GameObject Object;
}

