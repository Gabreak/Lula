using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class MouseManager : MonoBehaviour
{
    public static MouseManager Instance;

    [SerializeField] private Text _textInfo;
    //[SerializeField] private Vector2 _textShiftCenter;
    //private Camera _camera;


    private void Awake()
    {
        Instance = this;
        //_camera = Camera.main;
    }

    public void TextInfo(string text)
    {
        _textInfo.text = text;
    }
}
