using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class RandomBum : MonoBehaviour
{
    [Range(1, 100)]
    [SerializeField] private int _chance = 30;
    private void Start()
    {
        bool isBum = false;
        int rand = Random.Range(1, 100);
        if (rand <= _chance)
            isBum = true;

        gameObject.SetActive(isBum);
    }
}
