using Sirenix.OdinInspector;

using System;


using UnityEngine;
using UnityEngine.Localization;


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
    [Required]
    public LocalizedString Text;
}
