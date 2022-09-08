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
        _character.SetActive(BarManager.Instance.isWorking);
    }

    private IEnumerator DelayEnable()
    {
        _character.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        Tick(TimeManager.Instance.Hour);
    }
}
