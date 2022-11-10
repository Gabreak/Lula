using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Localization;

public class Message : MonoBehaviour
{
    [SerializeField] private LocalizedString _message;
    [SerializeField] private WindowIcon _icon;
    [SerializeField] private Color _textColor;
    private void OnMouseDown()
    {
        WindowMessage.Message(_message.GetLocalizedString(), _icon, _textColor);
    }
}
