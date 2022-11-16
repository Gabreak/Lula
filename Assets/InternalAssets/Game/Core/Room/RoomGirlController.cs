
using System;


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
        _isActive = (Convert.ToBoolean(_roomGirls.GameBeds[_indexGirl].Check));
        _check.SetActive(false);
        if (_isActive)
            OnCheck();
    }

    private void OnMouseDown()
    {
        _isActive = !_isActive;
        OnCheck();

    }

    private void OnCheck()
    {
        if (_isActive)
            _roomGirls.IdBinary[_indexGirl] = '1';
        else
            _roomGirls.IdBinary[_indexGirl] = '0';

        _roomGirls.GameBeds[_indexGirl].Check = Convert.ToInt32(_isActive);
        _roomGirls.UpdateBed(_indexGirl);

        _check.SetActive(_isActive);
    }
}
