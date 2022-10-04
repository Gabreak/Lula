using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ColorManager : MonoBehaviour
{
    [SerializeField] private int _time = 1;
    [SerializeField] private Image _color;
    public static ColorManager Instance;
    //private Coroutine _coroutine;

    public GameObject CurrentScene;
    public Transform DestroyScene;
    private bool _isClick = false;

    private void Awake()
    {
        Instance = this;
    }

    public void Play()
    {
        if (!_isClick)
        {
            _isClick = true;
            StartCoroutine(Disable());
        }
    }

    public IEnumerator Disable()
    {

        _color.enabled = true;
        float a = 0;
        float r = 0;
        float g = 0;
        float b = 0;
        while (a <= 1)
        {
            a += Time.deltaTime * _time;
            _color.color = new Color(r, g, b, a);
            yield return null;
        }
        if (CurrentScene != null && DestroyScene != null)
            OpenPrefab();

        while (a > 0.1f)
        {
            a -= Time.deltaTime * _time;
            _color.color = new Color(r, g, b, a);
            yield return null;
        }
        _color.enabled = false;
        _isClick = false;
        DestroyScene = null;
        CurrentScene = null;
        yield return null;
    }

    public void OpenPrefab()
    {
        LoadManager.OpenPrefab(DestroyScene, CurrentScene);
    }
}
