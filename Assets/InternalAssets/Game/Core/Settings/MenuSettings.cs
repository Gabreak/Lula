using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Reflection;

public class MenuSettings : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _resolution;
    [SerializeField] private TMP_Dropdown _quality;
    [SerializeField] private Toggle _fullScreen;

    private Resolution[] _resolutions;


    private void Start()
    {
        _resolutions = Screen.resolutions;

        _resolution.onValueChanged.AddListener(Drop);

        _fullScreen.onValueChanged.AddListener(FullScreen);
        _fullScreen.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("FullScreen", 1));
        Screen.fullScreen = _fullScreen.isOn;

        List<string> options = new List<string>();

        foreach (Resolution resolution in _resolutions)
        {
            string option = resolution.width + "x" + resolution.height + " "+ resolution.refreshRate +"Hz";
            options.Add(option);
        }
        _resolution.AddOptions(options);
        _resolution.onValueChanged.Invoke(PlayerPrefs.GetInt("DropResolution", _resolutions.Length - 1));

    }
    public void Drop(int index)
    {
        Debug.Log(index);
        Screen.SetResolution(_resolutions[index].width, _resolutions[index].height, _fullScreen.isOn);
        PlayerPrefs.SetInt("DropResolution",(index));
        _resolution.value = index;
    }

    public void FullScreen(bool isValue)
    {
        Screen.fullScreen = isValue;
        PlayerPrefs.SetInt("FullScreen", Convert.ToInt32(isValue));
    }
}
