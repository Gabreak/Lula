using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;
    public static UnityAction<bool> OnTick { get; set; }
    public static UnityAction<int> OnTickHour { get; set; }

    [SerializeField] private int _lightTime = 9;
    [SerializeField] private int _nightTime = 19;

    public float Speed = 1;
    public int Hour { get; set; }
    public int Minute { get; set; }


    private int _hour = 0;

    [SerializeField] private Text _textTime;
    [SerializeField] private Light _lightComponent;

    public void TakeSpeed(int speed)
    {
        Speed = speed;
    }

    private void Awake()
    {
        Instance = this;
        _hour = Hour;
        StartCoroutine(Time());
    }

    private IEnumerator Time()
    {
        while (true)
        {

            Sun(Hour, Minute);

            Hour += ++Minute / 60;

            if (Hour != _hour)
            {
                _hour = Hour;
                if (OnTickHour != null)
                    OnTickHour.Invoke(_hour);
            }

            if (Hour == 24 & Minute == 60)
            {
                Hour = 0;
                Minute = 0;
            }
            else if (Minute == 60)
                Minute = 0;


            _textTime.text = Hour.ToString("00") + ":" + Minute.ToString("00");


            yield return new WaitForSeconds(1 / Speed);
        }
    }

    private void Sun(float hour, float minute)
    {
        _lightComponent.transform.rotation = Quaternion.Euler(hour * 15f + minute / 4f + 180f, 0, 0);

        if (_lightTime == Hour && Minute == 0)
            OnTick(true);
        else if (_nightTime == Hour && Minute == 0)
            OnTick(false);
    }
}
