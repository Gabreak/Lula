using Sirenix.OdinInspector;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class LightHint : MonoBehaviour
{ 
    private Light _light;
    [SerializeField] private GameObject _background;
    [SerializeField] private Text _text;

    public Material[] MaterialsLight;
    //[SerializeField] private PostProcessVolume _post;
    [ListDrawerSettings(ShowIndexLabels =true,NumberOfItemsPerPage =20)]
    [SerializeField] private ObjectHelp[] _objects;
    private int _indexOld;


    private void Start()
    {
        _light = LightHubController.Instance.PostLight;
        _indexOld = PlayerPrefs.GetInt("Help" + transform.root.name, 0);
        if (_indexOld != -1)
            Play(_indexOld);
        else
            OnDisableBackground(false);
    }

    private void Play(int index)
    {
        _indexOld = index;
        PlayerPrefs.SetInt("Help" + transform.root.name, _indexOld);

        _objects[index].EventStart?.Invoke();
        if (_objects[index].Sprite != null)
        {
            _objects[index].Sprite.material = MaterialsLight[_objects[index].IndexMaterail];
        }
        _text.text = _objects[index].Text.GetLocalizedString();
        _light.color = Color.black;
    }
    public void OnNext()
    {
        int indexNext = _objects[_indexOld].NextIndex;
        _objects[_indexOld].EventEnd?.Invoke();
        if (indexNext == -1)
        {
            OnDisableBackground(true);
            return;
        }
        if (_objects[_indexOld].Sprite != null)
        {
            _objects[_indexOld].Sprite.material = MaterialsLight[_objects[_indexOld].IndexMaterailDefault];
        }
        Play(_indexOld + 1);
    }

    public void OnDisableBackground(bool isEnd)
    {
        LightHubController.Instance.PostLight.color = Color.white;
        if (_indexOld != -1)
        {
            if (_objects[_indexOld].Sprite != null)
                _objects[_indexOld].Sprite.material = MaterialsLight[_objects[_indexOld].IndexMaterailDefault];


            if (isEnd)
            {
                _indexOld = -1;
                PlayerPrefs.SetInt("Help" + transform.root.name, _indexOld);
            }
        }
        _background.SetActive(false);
    }
}


[Serializable]
public struct ObjectHelp
{
    public UnityEvent EventStart;
    public UnityEvent EventEnd;
    public SpriteRenderer Sprite;
    public LocalizedString Text;
    public int IndexMaterail;
    public int IndexMaterailDefault;
    public int NextIndex;
}
