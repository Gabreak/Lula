using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public static DoorManager Instance { get; private set; }
    public int Hour = 0;
    public int HourMax = 0;

    public int Price;
    public bool[] isDoor;

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
        else
            isDoor[0] = false;
    }
}
