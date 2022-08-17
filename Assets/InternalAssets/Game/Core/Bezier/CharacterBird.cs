using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[ExecuteAlways]
public class CharacterBird : MonoBehaviour
{
    [SerializeField] private Transform[] _pointers;
    [SerializeField] private GameObject _moneyText;

    [Range(0, 1)]
    [SerializeField] private float _time;


    private void Update()
    {
        _time = Mathf.Repeat(Time.time / 2, 1);
        Vector2 newPosition = Bezier.GetPoint(_pointers[0].position, _pointers[1].position, _pointers[2].position, _pointers[3].position, _time);

        transform.rotation = Bezier.GetRotationPoint(newPosition - (Vector2)transform.position);
        transform.position = newPosition;
    }

    private void OnDrawGizmos()
    {
        int number = 20;
        Vector2 prev = _pointers[0].position;
        for (int i = 0; i <= number; i++)
        {
            float parameter = (float)i / number;
            Vector2 point = Bezier.GetPoint(_pointers[0].position, _pointers[1].position, _pointers[2].position, _pointers[3].position, parameter);
            Gizmos.DrawLine(prev, point);
            prev = point;
        }
    }

    private void OnMouseDown()
    {
        Instantiate(_moneyText, transform.position, Quaternion.identity);
        MoneyProperties.Money += 1;
    }
}
