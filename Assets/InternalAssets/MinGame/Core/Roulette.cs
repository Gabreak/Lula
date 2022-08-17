using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class Roulette : MonoBehaviour
{
    public GameObject[] _prefab;
    [SerializeField] private Vector3 _defaultPosition;
    private GameObject[] _initPrefab;
    private Rigidbody2D _physics;
    [SerializeField] private float _radiusMax = 10;
    private float _radius = 1000;

    private void Start()
    {
        _physics = GetComponent<Rigidbody2D>();
        _initPrefab = new GameObject[_prefab.Length];
        for (int i = 0; i < _prefab.Length; i++)
        {

            _initPrefab[i] = Instantiate(_prefab[0], _defaultPosition, Quaternion.identity);
            transform.eulerAngles = new Vector3(0, 0, 360f / _prefab.Length * i + 1);
            _initPrefab[i].transform.parent = transform;
        }
    }

    private IEnumerator RotationCircle(float radius)
    {
        float speed = (Time.deltaTime * radius) / 2f;
        while (speed > 0.001f)
        {
            speed = (Time.deltaTime * radius) / 2f;
            radius -= speed;
            _physics.rotation += speed * _radiusMax;
            yield return null;

        }
        yield return null;
    }

    public void RotationRoulette()
    {
        StartCoroutine(RotationCircle(_radius));
    }

    private void Nearest()
    {
        _defaultPosition = transform.position;
    }
}
