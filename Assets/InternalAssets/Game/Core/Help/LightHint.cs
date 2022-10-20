using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Rendering.PostProcessing;

public class LightHint : MonoBehaviour
{
    [SerializeField] private PostProcessVolume _post;
    [SerializeField] private ObjectHelp[] _objects;

    private void Start()
    {
        _objects[0].Sprite.material = _objects[0].MaterialLight;
        //_light.color = Color.black; 
    }
}


[Serializable]
public struct ObjectHelp
{
    public SpriteRenderer Sprite;
    public Material MaterialLight;
    public Material MaterialDefault;
    public LocalizedString Header;
    public LocalizedString Text;
}
