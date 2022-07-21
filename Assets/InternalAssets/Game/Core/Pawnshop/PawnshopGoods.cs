using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PawnshopGoods : MonoBehaviour
{

    [SerializeField] private RedirectorPawnshop _redirecotor;



    private void OnEnable()
    {
        GoodsProperties.OnPawn += AddPawn;
        InstancePawn();
    }

    private void OnDisable()
    {
        GoodsProperties.OnPawn -= AddPawn;
        DestroyObjects();
    }

    private void AddPawn(DataProduct data)
    {
        PawnProperties.Instance.PawnshopGood.Datas.Add(data);
        GoodsProperties.Remove(data);
        PawnProperties.InitVisibleGood(_redirecotor, data, transform);
    }

    private void InstancePawn()
    {
        DataProduct[] data = PawnProperties.Instance.PawnshopGood.Datas.ToArray();
        for (int i = 0; i < data.Length; i++)
        {
            PawnProperties.InitVisibleGood(_redirecotor, data[i], transform);
        }
    }

    private void DestroyObjects()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            GameObject go = transform.GetChild(i).gameObject;
            Destroy(go);
        }
    }
}
