using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumMoney : MonoBehaviour
{
    [SerializeField] private int _money;
    private void OnMouseUp()
    {
        MoneyProperties.Money += _money;
        Destroy(gameObject);
    }
}
