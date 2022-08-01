using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class SensorGenerator : MonoBehaviour
{
    [SerializeField] private SensorRedirector PrefabSensor;
    private void OnEnable()
    {
        Daley();
    }

    private async void Daley()
    {
        await Task.Delay(1);
        BaseSensor[] sensors = GameDataBase.Instance.Sensor;

        foreach (BaseSensor sensor in sensors)
        {
            SensorRedirector redirecotr = Instantiate(PrefabSensor, transform);
            redirecotr.Info.TextInfo = sensor.Key.GetLocalizedString();
            redirecotr.ImageComponent.sprite = (sensor.Good != null) ? sensor.OK : sensor.Error;
            redirecotr.Sensor = sensor;

            transform.root.GetComponent<RoomSensor>().Sensors.Add(redirecotr);
        }
    }

    private void OnDisable()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
