using Sirenix.OdinInspector;

using System;
using System.Reflection;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.UI;

public class LightHintVideo : MonoBehaviour
{
    private Light _light;
    [SerializeField] private GameObject _background;
    [SerializeField] private Text _text;
    [SerializeField] private CharacterUpgradeVideo Character;

    public Material[] MaterialsLight;
    [ListDrawerSettings(ShowIndexLabels = true, ShowItemCount = true)]
    [SerializeField] private ObjectHelpVideo[] _objects;
    private int _indexOld;


    private void Start()
    {
        _indexOld = PlayerPrefs.GetInt("Help" + transform.root.name, 0);
        _light = LightHubController.Instance.PostLight;
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
        _text.text = _objects[index].Text.GetLocalizedString();
        _light.color = Color.black;

        Character.Rediretors[Character.CurrentLevel].Sprite.material = MaterialsLight[_objects[index].IndexLightMaterail];
    }
    public void OnNext()
    {
        int indexNext = _objects[_indexOld].NextIndex;
        if (indexNext == -1)
        {

            OnDisableBackground(true);
            return;
        }

        _objects[_indexOld].EventEnd?.Invoke();
        Character.Rediretors[Character.CurrentLevel].Sprite.material = MaterialsLight[_objects[_indexOld].IndexDefaultMaterail];
        Play(_indexOld + 1);
    }

    public void OnDisableBackground(bool isEnd)
    {
        LightHubController.Instance.PostLight.color = Color.white;
        if (_indexOld != -1)
        {
            _objects[_indexOld].EventEnd?.Invoke();
            Character.Rediretors[Character.CurrentLevel].Sprite.material = MaterialsLight[_objects[_indexOld].IndexDefaultMaterail];

            if (isEnd)
            {
                _indexOld = -1;
                PlayerPrefs.SetInt("Help" + transform.root.name, _indexOld);
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
public struct ObjectHelpVideo
{
    public UnityEvent EventStart;
    public UnityEvent EventEnd;
    public LocalizedString Text;
    [BoxGroup("Material")]
    public int IndexDefaultMaterail;
    [BoxGroup("Material")]
    public int IndexLightMaterail;

    public int NextIndex;
}
