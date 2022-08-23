using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "Girls", menuName = "GameData/Girl", order = 270)]
public class GirlsData : ScriptableObject
{
    public VideoGirl[] Videos;
    public GirlBase[] Girls;
}

[Serializable]
public struct GirlBase
{
    public bool IsActive;
    public string Name;
    public int Id;
    public GameObject Object;
}

[Serializable]
public struct VideoGirl
{
    public int IndexToy;
    public VideoClip[] Clip;
}

