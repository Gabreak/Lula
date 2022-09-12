using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private GameObject _room;

    [SerializeField] private int _roomNumber;
    [SerializeField] private int _price;

    [SerializeField] private CanvasInfo _canvasInfo;


    public void TransitionRoom()
    {
        int index = PlayerPrefs.GetInt(transform.root.name + _roomNumber, -1);
        if (index == _roomNumber)
        {
            LoadManager.OpenPrefab(transform.root, _room);
        }
        else
        {
            DoorRedirector rooms = Instantiate(_canvasInfo.Panel, _canvasInfo.Parent.transform);
            rooms.TextMoney.text = _price + "$";
            rooms.Prefab = _room;
            rooms.RoomNumber = _roomNumber;
            rooms.IndexGirl = _roomNumber - 1;
            rooms.Money = _price;
            ControlSystemProperties.DisableInvoke();

        }
    }
    [Serializable]
    public struct CanvasInfo
    {
        public Canvas Parent;
        public DoorRedirector Panel;
    }

}
