using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class DoorGirl : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _indexDoor;
    public void SetMoney(int money)
    {
        RoomsRedirector rooms = transform.root.GetComponent<RoomsRedirector>();
        rooms.TextMoney.text = money + "$";
        rooms.Prefab = _prefab;
        rooms.IndexDoor = _indexDoor;
        rooms.Money = money;
    }

    public void OpenPrefabs()
    {
        if (DoorManager.Instance.isDoor[_indexDoor])
        {
            LoadManager.OpenPrefab(transform.root, _prefab);
        }
    }

}
