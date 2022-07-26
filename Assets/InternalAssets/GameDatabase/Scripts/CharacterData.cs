using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;


[CreateAssetMenu(fileName = "Charaters", menuName = "GameData/CharaterUpgraude", order = 280)]
public class CharacterData : ScriptableObject
{
    public DataCharacter[] Character;
}

[Serializable]
public struct DataCharacter
{
    public CharacterUpgrade Prefab;
    public bool InRoom;
    public int Id;
}
