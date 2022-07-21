using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pawnshop", menuName = "GameData/Pawnshop", order = 150)]
public class PawnShopBase : ScriptableObject
{
    public List<DataProduct> Datas;
}
