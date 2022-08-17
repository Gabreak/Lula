using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class RoomsRedirector : MonoBehaviour
{
    public Text TextMoney;
    public GameObject Prefab;
    public int IndexDoor;
    public int Money;

    public void OpenPrefab()
    {
        LoadManager.OpenPrefab(transform.root, Prefab);
    }

    public void BuyDoor()
    {
        if (MoneyProperties.Money >= Money)
        {
            DoorManager.Instance.isDoor[IndexDoor] = true;
            MoneyProperties.Money -= Money;

            OpenPrefab();
        }
    }
}
