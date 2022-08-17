using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ChildController : MonoBehaviour
{
    [SerializeField] private bool isActive = false;

    private void OnEnable()
    {
        ChildActive();
    }

    public void ChildActive()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(isActive);
        }

    }
}
