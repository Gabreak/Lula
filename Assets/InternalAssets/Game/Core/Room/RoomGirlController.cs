using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class RoomGirlController : MonoBehaviour
{
    public int IdBinary = 0;
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
        {
            _check.SetActive(true);
            _roomGirls.IdBinary += IdBinary;
        }
        else
        {
            _check.SetActive(false);
            _roomGirls.IdBinary -= IdBinary;
        }
    }
}
