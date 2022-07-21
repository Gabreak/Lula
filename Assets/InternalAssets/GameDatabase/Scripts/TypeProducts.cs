using System.Collections;
using System.Collections.Generic;

using UnityEngine;


[CreateAssetMenu(fileName = "Type", menuName = "GameData/Type", order = 50)]
public class TypeProducts : ScriptableObject
{
    [SerializeField] private string _name = "Type";
    public int Index = 0;
    public int SelectId = -1;

    public List<GameGoods> Acquired;
}
