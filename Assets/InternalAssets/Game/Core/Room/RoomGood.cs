using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class RoomGood : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private TypeProducts _type;
    public int SelectedId { get; set; } = -1;

    private ToggleGroup _toggleGroup;

    private void OnEnable()
    {
        _toggleGroup = GetComponent<ToggleGroup>();
        for (int i = 0; i < _type.Acquired.Count; i++)
        {
            RoomGoodRedirector room = Instantiate(_prefab, transform).GetComponent<RoomGoodRedirector>();
            room.ToggleComponent.group = _toggleGroup;
            room.Good = _type.Acquired[i];
            //Debug.Log(_type.SelectId == room.Good.Id);
            if (_type.SelectId == room.Good.Id)
            {
                SelectedId = _type.SelectId;
                room.ToggleComponent.isOn = true;
            }
            room.Lable.text = _type.Acquired[i].Key.GetLocalizedString();
        }
    }
    private void OnDisable()
    {
        for (int i = _type.Acquired.Count - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    public void SetGoodId()
    {
        _type.SelectId = SelectedId;
    }
}
