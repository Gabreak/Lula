using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GirlController : MonoBehaviour
{
    [SerializeField] private VideoGirlsData _data;

    private void Start()
    {
        for (int i = 1; i < _data.Girls.Length; i++)
            _data.Girls[i].IsActive = Convert.ToBoolean(PlayerPrefs.GetInt("IsActiveGirl" + i, 0));
    }

    public void InviteGirl(int indexGirl)
    {
        _data.Girls[indexGirl].IsActive = true;
        PlayerPrefs.SetInt("IsActiveGirl" + indexGirl, 1);

    }

}
