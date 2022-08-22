using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ObjectRandomChild : MonoBehaviour
{
    private void OnEnable()
    {

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        transform.GetChild(Random.Range(0, transform.childCount)).gameObject.SetActive(true);
    }
}
