using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class LoadGame : MonoBehaviour
{
    private void Start()
    {
        if (!PlayerPrefs.HasKey("Goods"))
            gameObject.SetActive(false);
    }
}
