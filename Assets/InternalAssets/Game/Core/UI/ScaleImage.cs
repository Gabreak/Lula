using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class ScaleImage : MonoBehaviour
{
    [SerializeField] private RectTransform _target;
    //[SerializeField] private float _scale;
    void Update()
    {
        float distance = Vector3.Distance(_target.position, transform.position) / (Screen.width /4);
        transform.localScale = new Vector3(1f - distance, 1f - distance, 1f);
    }
}
