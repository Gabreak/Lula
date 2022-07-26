using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class RoomGoodRedirector : MonoBehaviour
{
    public Toggle ToggleComponent;
    public Text Lable;
    public GameGoods? Good;

    private RoomGood _parentRoom;

    private void Start()
    {
        ToggleComponent.onValueChanged.AddListener(AddToggle);
        _parentRoom = transform.parent.GetComponent<RoomGood>();
    }

    private void AddToggle(bool isActive)
    {
        _parentRoom.SelectGood = null;
        if (isActive)
            _parentRoom.SelectGood = Good;
        //else if (_parentRoom.Sensor.Good.Value == Good)
        //    _parentRoom.SelectedId = -1;
    }
}
