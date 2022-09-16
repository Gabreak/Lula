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
    private Coroutine _coroutine;

    public GameObject CurrentScene;
    public Transform DestroyScene;

    private void Awake()
    {
        Instance = this;
    }

    public void Play()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Disable());
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

        OpenPrefab();
        while (a > 0)
        {
            a -= Time.deltaTime * _time;
            _color.color = new Color(r, g, b, a);
            yield return null;
        }

        _color.enabled = false;
        yield return null;
    }

    public void OpenPrefab()
    {
        if (DestroyScene != null)
            LoadManager.OpenPrefab(DestroyScene, CurrentScene);
        else
            LoadManager.OpenPrefab(CurrentScene);
    }
}
