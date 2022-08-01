using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "VideoRecord", menuName = "GameData/VideoRecord", order = 220)]
public class BaseRecord : ScriptableObject
{
    public List<VideoRecord> Records;
}

[Serializable]
public struct VideoRecord
{
    public int Price;
    public string Name;
    public VideoClip Clip;
}
