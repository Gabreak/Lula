using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] private GameObject _settingPanel;

    public void EnablePanel()
    {
        _settingPanel.SetActive(!_settingPanel.activeSelf);
    }
}
