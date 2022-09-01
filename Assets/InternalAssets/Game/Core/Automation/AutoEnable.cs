using System;

using UnityEngine;

public class AutoEnable : MonoBehaviour
{
    [SerializeField] private ObjectEnable[] _objects;

    private void OnEnable()
    {
        foreach (var obj in _objects)
            obj.Object.SetActive(obj.IsEnable);
    }
}

[Serializable]
public struct ObjectEnable
{
    public GameObject Object;
    public bool IsEnable;
}
