using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class TestQueue : MonoBehaviour
{
    private Queue<int> _position;

    void Start()
    {
        _position = new Queue<int>();
        _position.Enqueue(0);
        _position.Enqueue(1);
        Debug.Log(_position.Dequeue());
        Debug.Log(_position.Dequeue());
        bool isResult = _position.TryDequeue(out int result);
        Debug.Log(isResult);


    }
}
