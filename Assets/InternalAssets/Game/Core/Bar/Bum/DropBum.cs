using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class DropBum : MonoBehaviour
{
    [SerializeField] private BaseProduct _bumItem;


    private void OnMouseUp()
    {
        _bumItem.Type.Acquired.Add(_bumItem.Goods[ToiletBum.IndexItem]);
        Destroy(gameObject);
    }
}
