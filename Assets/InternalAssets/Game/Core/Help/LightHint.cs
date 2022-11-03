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
    [Required]
    [SerializeField] private GameObject _background;
    [Required]
    [SerializeField] private Button _buttonNext;
    [Required]
    [SerializeField] private Text _text;

    public Material[] MaterialsLight;
    //[SerializeField] private PostProcessVolume _post;
    [ListDrawerSettings(ShowIndexLabels = true, NumberOfItemsPerPage = 20)]
    [SerializeField] private ObjectHelp[] _objects;
    [HideInInspector] public int IndexOld;


    private void Start()
    {
        _light = LightHubController.Instance.PostLight;
        IndexOld = PlayerPrefs.GetInt("Help" + transform.root.name, 0);
        if (IndexOld != -1)
            Play(IndexOld);
        else
            OnDisableBackground(false);
    }

    public void OnReset()
    {
        IndexOld = PlayerPrefs.GetInt("Help" + transform.root.name, 0);

        if (IndexOld != -1)
        {
            Play(IndexOld);
            _background.SetActive(true);
        }
        else
            OnDisableBackground(false);
    }

    public void Play(int index)
    {
        IndexOld = index;

        _buttonNext.gameObject.SetActive(_objects[index].IsNextButton);
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
        int indexNext = _objects[IndexOld].NextIndex;
        _objects[IndexOld].EventEnd?.Invoke();
        if (indexNext == -1)
        {
            OnDisableBackground(true);
            return;
        }
        if (_objects[IndexOld].Sprite != null)
        {
            _objects[IndexOld].Sprite.material = MaterialsLight[_objects[IndexOld].IndexMaterailDefault];
        }
        Play(IndexOld + 1);
    }

    public void OnSave(int index)
    {
        OnDisableBackground(false);
        PlayerPrefs.SetInt("Help" + transform.root.name, index);
    }

    public void OnNextSave(int index)
    {
        if (_objects[IndexOld].Sprite != null)
        {
            _objects[IndexOld].Sprite.material = MaterialsLight[_objects[IndexOld].IndexMaterailDefault];
        }
        PlayerPrefs.SetInt("Help" + transform.root.name, index);
        Play(index);
    }

    public void OnDisableBackground(bool isEnd)
    {

        LightHubController.Instance.PostLight.color = Color.white;
        if (IndexOld != -1)
        {
            if (_objects[IndexOld].Sprite != null)
                _objects[IndexOld].Sprite.material = MaterialsLight[_objects[IndexOld].IndexMaterailDefault];


            if (isEnd)
            {
                IndexOld = -1;
                PlayerPrefs.SetInt("Help" + transform.root.name, IndexOld);
            }
        }
        _background.SetActive(false);
    }
    private void OnDisable()
    {
        OnDisableBackground(false);
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
    public bool IsNextButton;
    [ShowIf("IsNextButton")]
    public int NextIndex;
}
