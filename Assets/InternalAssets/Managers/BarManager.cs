using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.TextCore.Text;

public class BarManager : MonoBehaviour
{
    public CharacterDataBase CharacterData;

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
            int end = CharacterData.Working[index].End;
            if (end == hour)
                MoneyProperties.Money += CharacterData.Working[index].Profit;
        }
    }
}
