using System.Collections;
using System.Collections.Generic;

using UnityEngine;


[CreateAssetMenu(fileName = "Type", menuName = "GameData/Type", order = 50)]
public class TypeProducts : ScriptableObject
{
    public string Name = "Type";
    public int Index = 0;

    public List<GameGoods> Acquired;
}
