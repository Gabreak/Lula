using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class LockDoor : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _doorIndex = 0;
    
    private void OnMouseDown()
    {
        if (DoorManager.Instance.isDoor[_doorIndex])
            LoadManager.OpenPrefab(transform.root, _prefab);
    }

}
