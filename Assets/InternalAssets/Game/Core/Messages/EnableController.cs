using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EnableController : MonoBehaviour
{
    public GameObject controller;
    public void EnableDisable()
    {
        controller.SetActive(!controller.activeSelf);
    }
}
