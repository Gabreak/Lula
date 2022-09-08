using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[ExecuteAlways]
public class CharacterBird : MonoBehaviour
{
    [SerializeField] private Transform[] _pointers;
    [SerializeField] private TextMesh _moneyText;

    [Range(0, 1)]
    [SerializeField] private float _time;
    [SerializeField] private int _money = 1;


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
        TextMesh text = Instantiate(_moneyText, transform.position, Quaternion.identity);
        text.text = _money + "$";

        MoneyProperties.Money += _money;
        int money = Random.Range(0, 5 + _money) < 5 ? 1 : -1;
        _money += money;
    }
}
