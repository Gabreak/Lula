using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName ="Video",menuName ="GameData/Video",order =210)]
public class VideoData : ScriptableObject
{
    public VideoBase[] Video;
}

[Serializable]
public struct VideoBase
{
    public VideoClip[] Clip;
}

