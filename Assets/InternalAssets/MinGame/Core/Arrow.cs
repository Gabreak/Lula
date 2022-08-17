using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using MinGame;

using Unity.Mathematics;

using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float _rotationMax = 10;

    private Rigidbody2D _physics;
    private float _rotationSpeed = 1000;
    private bool _isRotation = true;



    private void Start()
    {
        _physics = GetComponent<Rigidbody2D>();
    }

    private IEnumerator StartRotation(float radius)
    {
        _isRotation = false;
        float max = UnityEngine.Random.Range(_rotationMax - (_rotationMax / 2f), _rotationMax + (_rotationMax / 2f));
        float speed = (Time.deltaTime * radius) / 2f;
        while (speed > 0.001f)
        {
            speed = (Time.deltaTime * radius) / 2f;
            radius -= speed;
            _physics.rotation += speed * max;
            yield return null;
        }
        Nearest();
        _isRotation = true;
        yield return null;
    }

    public void StarRotation()
    {
        if (_isRotation)
            StartCoroutine(StartRotation(_rotationSpeed));
    }

    private void Nearest()
    {
        FortuneGameData fortune = FortuneGameData.Instane;
        PresentAnimation[] prefabs = fortune.Presents;
        float z = transform.eulerAngles.z;

        var minPrefabs = from p in prefabs
                         where p.transform.eulerAngles.z > z - fortune.Diff / 2f && p.transform.eulerAngles.z < z + fortune.Diff / 2f
                         select p;

        foreach (var prefab in minPrefabs)
        {
            prefab.Play();
        }
    }
}
