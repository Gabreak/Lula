using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ControlMonoSystem : MonoBehaviour
{
    [SerializeField] private bool _isOpen = false;
    private void OnEnable()
    {
        if (_isOpen)
            ControlSystemProperties.DisableInvoke();
    }

    public void Enable() => ControlSystemProperties.EnableInvoke();

    public void Disable() => ControlSystemProperties.DisableInvoke();
}
