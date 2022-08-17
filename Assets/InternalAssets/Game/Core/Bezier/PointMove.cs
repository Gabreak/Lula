using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PointMove : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _y;
    private void Update()
    {
        transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time * _speed, _y * 2) - _y, transform.position.z);

    }
}
