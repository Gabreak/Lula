using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ControlSystem : MonoBehaviour
{
    [SerializeField] private bool _isBoxCollsion = false;
    [SerializeField] private bool _isObject = true;
    private BoxCollider2D _collision;

    private bool _isDefaultCollision;
    private bool _isDefaultObject;

    private void Start()
    {
        _collision = GetComponent<BoxCollider2D>();

        if (_collision != null)
            _isDefaultCollision = _collision.enabled;
        _isDefaultObject = gameObject.activeSelf;

    }

    private void OnEnable()
    {
        ControlSystemProperties.DisableHandle += Disable;
        ControlSystemProperties.EnableHandle += Enable;
    }

    private void OnDisable()
    {
        ControlSystemProperties.DisableHandle -= Disable;
        ControlSystemProperties.EnableHandle -= Enable;
    }

    private void Disable()
    {
        if (_collision != null)
        {
            _isDefaultCollision = _collision.enabled;
            _collision.enabled = _isBoxCollsion;
        }

        _isDefaultObject = gameObject.activeSelf;
        gameObject.SetActive(_isObject);
    }

    private void Enable()
    {
        if (_collision != null)
        {
            _collision.enabled = true;
        }
        gameObject.SetActive(_isDefaultObject);
    }
}
