using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class RoomGirls : MonoBehaviour
{
    [SerializeField] private VideoGirlsData _data;
    private int _idBinary = 0;
    public int IdBinary
    {
        get => _idBinary;
        set
        {
            _idBinary = value;
            UpdateBinary();
        }
    }

    private void Start()
    {

    }

    public void UpdateBinary()
    {
        _data.IndexVideo = (Convert.ToInt32(IdBinary.ToString(), 2));

    }

}
