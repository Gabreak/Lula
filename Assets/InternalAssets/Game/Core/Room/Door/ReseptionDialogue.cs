using System;

using UnityEngine;
using UnityEngine.Localization;

public class ReseptionDialogue : MonoBehaviour
{
    [SerializeField] private GameObject[] _dialogue;
    [SerializeField] private LocalizedString _good;
    [SerializeField] private TaskClick _task;

    private void OnEnable()
    {
        int index = Convert.ToInt32(DoorManager.Instance.isDoor);
        _dialogue[index].SetActive(true);
    }

    private void OnDisable()
    {
        for (int i = 0; i < _dialogue.Length; i++)
        {
            _dialogue[i].SetActive(false);
        }
    }

    public void DoorTake()
    {
        DoorManager door = DoorManager.Instance;
        if (door.HourMax != 0 && MoneyProperties.Money >= door.Price)
        {
            door.isDoor = true;
            door.Hour = door.HourMax;
            door.HourMax = 0;
            _task?.Action(0);
            MoneyProperties.Money -= door.Price;
            //WindowMessage.Message(_good.GetLocalizedString(), WindowIcon.Information);
        }
        else if (door.HourMax == 0)
        {

        }
        else
            MoneyProperties.NoMoneyMessage();

    }

    public void DoorClose()
    {
        DoorManager.Instance.isDoor = false;
        DoorManager.Instance.Hour = 0;
    }

}
