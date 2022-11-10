using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WindowMessage : MonoBehaviour
{
    [SerializeField] private Sprite[] _icon;
    [SerializeField] private GameObject _panel;
    [SerializeField] private Image _typeWindow;
    [SerializeField] private TextMeshProUGUI _text;

    private static WindowMessage _instance;

    private void Awake()
    {
        _instance = this;
    }

    public static void Message(string text, WindowIcon icon, Color color = default)
    {
        _instance._typeWindow.color = (color == default) ? Color.white : color;
        _instance._typeWindow.sprite = _instance._icon[(int)icon];

        _instance._text.text = text;

        _instance._panel.SetActive(true);
    }
}

public enum WindowIcon { Warning, Information }
