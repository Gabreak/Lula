using System.Collections;
using System.Collections.Generic;

using UnityEngine;


[CreateAssetMenu(fileName = "Music", menuName = "GameData/Music", order = 250)]
public class BaseAudio : ScriptableObject
{
    public List<AudioClip> Audios;
}
