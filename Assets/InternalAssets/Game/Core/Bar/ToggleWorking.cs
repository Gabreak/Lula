using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class ToggleWorking : MonoBehaviour
{
    private Toggle _toggle;
    [SerializeField] private bool _isWorking = false;
    [SerializeField] private int _index;

    void Start()
    {
        _toggle = GetComponent<Toggle>();
        _toggle.onValueChanged.AddListener(ToggleApply);

        if (BarManager.Instance.CharacterData.IndexTime == _index)
            _toggle.isOn = true;

    }

    private void ToggleApply(bool active)
    {
        if (_index >= 0)
        {
            TaskClick task = transform.parent.GetComponent<TaskClick>();
            task?.Action(0);
        }
        BarManager.Instance.CharacterData.IsWorking = _isWorking;
        BarManager.Instance.CharacterData.IndexTime = _index;
    }
}
