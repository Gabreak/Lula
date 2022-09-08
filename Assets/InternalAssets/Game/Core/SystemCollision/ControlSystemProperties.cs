using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public static class ControlSystemProperties
{
    public static UnityAction DisableHandle;
    public static UnityAction EnableHandle;

    public static void DisableInvoke()
    {
        DisableHandle?.Invoke();
    }

    public static void EnableInvoke()
    {
        EnableHandle?.Invoke();
    }

}
