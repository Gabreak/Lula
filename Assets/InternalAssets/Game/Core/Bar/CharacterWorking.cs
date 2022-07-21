using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CharacterWorking : MonoBehaviour
{
    [SerializeField] private GameObject _character;

    private void Start()
    {
    }

    private void OnEnable()
    {
        TimeManager.OnTickHour += Tick;
        StartCoroutine(DelayEnable());
    }

    private void OnDisable()
    {
        TimeManager.OnTickHour -= Tick;
    }

    private void Tick(int hour)
    {
        int index = BarManager.Instance.CharacterData.IndexTime;

        if (index >= 0)
        {
            int begin = BarManager.Instance.CharacterData.Working[index].Begin;
            int end = BarManager.Instance.CharacterData.Working[index].End;
            if (begin == hour)
                _character.SetActive(true);
            else if (end == hour)
                _character.SetActive(false);
        }
    }
    private IEnumerator DelayEnable()
    {
        _character.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        Tick(TimeManager.Instance.Hour);
    }
}
