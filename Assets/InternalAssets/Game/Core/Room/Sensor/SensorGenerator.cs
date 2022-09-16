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
        List<SensorRedirector> redirectorSensor = transform.root.GetComponent<RoomSensor>().Sensors;
        redirectorSensor.Clear();
        foreach (BaseSensor sensor in sensors)
        {
            SensorRedirector redirector = Instantiate(PrefabSensor, transform);
            redirector.Info.TextInfo = sensor.Key.GetLocalizedString();
            redirector.ImageComponent.sprite = (sensor.Good != null) ? sensor.OK : sensor.Error;
            redirector.Sensor = sensor;

            redirectorSensor.Add(redirector);
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
