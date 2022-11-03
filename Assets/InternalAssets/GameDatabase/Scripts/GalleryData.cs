using Sirenix.OdinInspector;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Galley", menuName = "GameData/Galley", order = 10)]
public class GalleryData : ScriptableObject
{
    [ListDrawerSettings(ShowIndexLabels = true)]
    public GalleryBase[] Bases;
}

[Serializable]
public struct GalleryBase
{
    public Sprite Image;
    public LocalizedString Text;
}
