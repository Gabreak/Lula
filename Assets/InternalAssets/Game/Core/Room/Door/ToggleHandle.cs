using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class ToggleHandle : MonoBehaviour
{
    [SerializeField] private int _timeRoom;
    [SerializeField] private int _price;
    private Toggle _toggle;
    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
        _toggle.onValueChanged.AddListener(SetToggle);
    }
    private void OnEnable()
    {
        _toggle.isOn = false;
    }

    private void SetToggle(bool isLock)
    {
        DoorManager.Instance.HourMax = isLock ? _timeRoom : 0;
        DoorManager.Instance.Price = _price;
    }
}
