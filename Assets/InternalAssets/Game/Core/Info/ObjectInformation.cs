using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ObjectInformation : MonoBehaviour
{
    public string TextInfo { get; set; }


    private void Start()
    {
        MouseManager.Instance.TextInfo("");
    }

    private void OnMouseEnter()
    {
        MouseManager.Instance.TextInfo(TextInfo);
    }

    private void OnMouseExit()
    {
        MouseManager.Instance.TextInfo("");
    }
}
