using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.TextCore.Text;

public class BarManager : MonoBehaviour
{
    public CharacterDataBase CharacterData;
    public bool isWorking = false;
    public static BarManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        TimeManager.OnTickHour += LulaWorking;

    }

    private void OnDisable()
    {
        TimeManager.OnTickHour -= LulaWorking;

    }

    private void LulaWorking(int hour)
    {
        int index = CharacterData.IndexTime;

        if (index >= 0)
        {

            int begin = CharacterData.Working[index].Begin;
            int end = CharacterData.Working[index].End;

            if (begin == hour)
                isWorking = true;
            else if (end == hour)
                isWorking = false;

            if (end == hour)
                MoneyProperties.Money += CharacterData.Working[index].Profit;
        }
        else if (isWorking == true)
            isWorking = false;
    }
}
