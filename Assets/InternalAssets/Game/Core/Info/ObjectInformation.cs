using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectInformation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string TextInfo { get; set; } = "";


    private void Start() => MouseManager.Instance.TextInfo("");

    private void OnMouseEnter() => MouseManager.Instance.TextInfo(TextInfo);


    private void OnMouseExit() => MouseManager.Instance.TextInfo("");

    public void OnPointerEnter(PointerEventData eventData) => MouseManager.Instance.TextInfo(TextInfo);

    public void OnPointerExit(PointerEventData eventData) => MouseManager.Instance.TextInfo("");

}
