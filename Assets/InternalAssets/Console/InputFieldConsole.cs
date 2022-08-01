using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputFieldConsole : MonoBehaviour
{
    private TMP_InputField _field;
    private void Awake()
    {
        _field = GetComponent<TMP_InputField>();
    }

    private void OnEnable()
    {
        _field.ActivateInputField();
    }
}
