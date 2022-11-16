using Sirenix.OdinInspector;

using System;


using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "Girls", menuName = "GameData/Girl", order = 270)]
public class VideoGirlsData : ScriptableObject
{
    public int IndexVideo;
    [ListDrawerSettings(ShowIndexLabels = true)]
    public VideoGirl[] Videos;
    public GirlBase[] Girls;
}

[Serializable]
public struct GirlBase
{
    public bool IsActive;
    public string Name;
    public int Id;
    public RoomGirlController Object;
}

[Serializable]
public struct VideoGirl
{
    public int IndexToy;
    public VideoClip[] Clip;
    [Required]
    public LocalizedString Warning;
    [Required]
    public LocalizedString WarningToy;
}

