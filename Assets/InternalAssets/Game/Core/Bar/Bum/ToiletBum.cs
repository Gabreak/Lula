using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ToiletBum : MonoBehaviour
{
    [SerializeField] private BaseProduct _drop;
    [SerializeField] private BumRedirector _bum;

    public static int IndexItem { get; set; }
    public void CreateObject()
    {
        IndexItem = Random.Range(0, _drop.Goods.Length);
        Instantiate(_drop.Goods[IndexItem].Product, transform);
        //_bum.BumAnimator.SetBool("Hit", true);
        _bum.ComponentDisable();

        PoliceManager.Instance.LoadJail(transform.root.gameObject,10);
    }
}
