using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ClickNext : MonoBehaviour
{
    [SerializeField] private LightHint _hint;
    [SerializeField] private int _currentIndex;
    [SerializeField] private int _nextIndex;

    private void OnMouseDown()
    {
        Debug.Log(_hint.IndexOld + ":" + _currentIndex);
        if (_hint.IndexOld == _currentIndex)
        {
            _hint.OnSave(_nextIndex);
        }
    }
}
