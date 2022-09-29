using JetBrains.Annotations;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class RoomGirlController : MonoBehaviour
{
    public int IdBinary = 0;
    [SerializeField] private int _indexGirl;
    [SerializeField] private GameObject _check;
    private bool _isActive = false;

    private RoomGirls _roomGirls;
    private void Start()
    {
        _roomGirls = transform.root.GetComponent<RoomGirls>();
        _check.SetActive(false);
    }

    private void OnMouseDown()
    {
        _isActive = !_isActive;

        if (_isActive)
            _roomGirls.IdBinary += IdBinary;
        else
            _roomGirls.IdBinary -= IdBinary;

        _roomGirls._gameBed[_indexGirl].Check = Convert.ToInt32(_isActive);
        _roomGirls.UpdateBed(_indexGirl);

        _check.SetActive(_isActive);
    }
}
