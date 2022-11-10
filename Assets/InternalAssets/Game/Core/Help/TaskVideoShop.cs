using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class TaskVideoShop : MonoBehaviour
{
    [SerializeField] private TypeProducts[] _products;
    [SerializeField] private TasksPrograss[] _tasks;

    public void TestBuyProduct()
    {
        int count = 0;
        for (int i = 0; i < _products.Length; i++)
        {
            if (_products[i].Acquired.Count > 0)
            {
                count++;
            }
        }
        TaskManager.Instance.OnProgress(_tasks, count);
    }
}
