using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class RoomGood : MonoBehaviour
{
    [SerializeField] private RoomGoodRedirector _prefab;
    public BaseSensor Sensor;
    public GameGoods? SelectGood { get; set; }

    private ToggleGroup _toggleGroup;

    private void OnEnable()
    {
        _toggleGroup = GetComponent<ToggleGroup>();
        for (int i = 0; i < Sensor.Type.Acquired.Count; i++)
        {
            RoomGoodRedirector redirector = Instantiate(_prefab, transform);
            redirector.ToggleComponent.group = _toggleGroup;
            redirector.Good = Sensor.Type.Acquired[i];

            //string nameNormal = GameDataBase.Instance.UsbSebsor.Type.Acquired.Select(g => g.Level >= redirector.Good.Value.Level)
            IEnumerable sizeNoraml = from g in GameDataBase.Instance.UsbSebsor.Type.Acquired
                                     where g.Level >= redirector.Good.Value.Level
                                     select g;
            foreach (GameGoods good in sizeNoraml)
            {
                //good
            }

            redirector.Lable.text = $"Usb Size: { redirector.Good.Value.Level} " + Sensor.Type.Acquired[i].Key.GetLocalizedString();
            if (Sensor.Good != null)
            {
                if (Sensor.Good.Value.Id == redirector.Good.Value.Id)
                {
                    redirector.ToggleComponent.isOn = true;
                }
            }
        }

    }
    private void OnDisable()
    {
        for (int i = Sensor.Type.Acquired.Count - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    public void UpdateSensor()
    {

        //_sensor.SelectId = SelectedId;
        RoomSensor roomSensor = transform.root.GetComponent<RoomSensor>();
        BaseSensor typeUsb = GameDataBase.Instance.UsbSebsor;
        foreach (var sensor in roomSensor.Sensors)
        {
            Sensor.Good = SelectGood;
            if (sensor.Sensor.Type.Index == Sensor.Type.Index)
            {
                if (typeUsb.Type.Index != Sensor.Type.Index)
                {

                    if (typeUsb.Good != null)
                    {

                        if (typeUsb.Good.Value.Level < sensor.Sensor.Good.Value.Level)
                        {
                            UsbReset(typeUsb, roomSensor);
                        }
                    }
                }
                else
                {
                    int max = roomSensor.Sensors.Max(m =>
                    (m.Sensor.Good != null &&
                    m.Sensor.Type.Index != typeUsb.Type.Index) ?
                    m.Sensor.Good.Value.Level :
                    0);
                    Debug.Log(typeUsb.Good.Value.Level);
                    if (max > typeUsb.Good.Value.Level)
                    {
                        Sensor.Good = null;
                    }

                }

                sensor.ImageComponent.sprite = (Sensor.Good != null) ? sensor.Sensor.OK : sensor.Sensor.Error;
            }
        }
    }

    private void UsbReset(BaseSensor usb, RoomSensor sensor)
    {
        usb.Good = null;

        SensorRedirector sensorUsb = (from s in sensor.Sensors
                                      where s.Sensor.Type.Index == usb.Type.Index
                                      select s).First();
        sensorUsb.ImageComponent.sprite = sensorUsb.Sensor.Error;
    }
}
