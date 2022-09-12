using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DoorRedirector : MonoBehaviour
{
    public Text TextMoney;
    public GameObject Prefab { get; set; }
    public int RoomNumber { get; set; }
    public int Money { get; set; }

    public UnityAction CloseHandle;

    private void RoomOpen()
    {
        LoadManager.OpenPrefab(transform.root, Prefab);
    }

    public void BuyDoor()
    {
        if (MoneyProperties.Money >= Money)
        {
            PlayerPrefs.SetInt(transform.root.name + RoomNumber, RoomNumber);
            MoneyProperties.Money -= Money;

            RoomOpen();
        }
    }


}
