using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class ObjectTransition : MonoBehaviour
{
    [SerializeField] private UnityEvent _onMouseDown;

    private void OnMouseUp() => Close();

    public void Close() => _onMouseDown.Invoke();
}
