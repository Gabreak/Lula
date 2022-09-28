using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ObjectRandomChild : MonoBehaviour
{
    [SerializeField] private VideoGirlsData _data;
    private void OnEnable()
    {

        for (int i = 1; i < _data.Girls.Length; i++)
        {
            if (!_data.Girls[i].IsActive)
            {
                 
                transform.GetChild(i - 1).gameObject.SetActive(true);
                break;
            }
        }

        //transform.GetChild(Random.Range(0, transform.childCount)).gameObject.SetActive(true);
    }
}
