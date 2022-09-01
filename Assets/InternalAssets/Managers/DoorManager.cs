using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Localization;

public class DoorManager : MonoBehaviour
{
    public static DoorManager Instance { get; private set; }
    public int Hour = 0;
    public int HourMax = 0;

    public int Price;
    public bool isDoor;
    [SerializeField] private LocalizedString _rent;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        TimeManager.OnTickHour += TimeHour;
    }

    private void OnDisable()
    {
        TimeManager.OnTickHour -= TimeHour;
    }

    private void TimeHour(int hour)
    {
        if (Hour > 0)
            Hour--;
        else if (isDoor)
        {
            WindowMessage.Message(_rent.GetLocalizedString(),WindowIcon.Information);
            isDoor = false;
        }
    }
}
