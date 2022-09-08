using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class TextMoneyMesh : MonoBehaviour
{
    private TextMesh _mesh;
    private float _alpha = 1;
    private void Start()
    {
        _mesh = GetComponent<TextMesh>();
    }
    private void Update()
    {
        transform.position += new Vector3(0, Time.deltaTime*2, 0);
        _alpha -= Time.deltaTime / 2f;
        _mesh.color = new Color(1, 1, 1, _alpha);
        if (_alpha <= 0)
            Destroy(gameObject);
    }
}
